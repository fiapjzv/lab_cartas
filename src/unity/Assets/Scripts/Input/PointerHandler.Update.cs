using UnityEngine;
using UnityEngine.InputSystem;

public partial class PointerHandler
{
    public void Update()
    {
        if (_pointer.press.wasPressedThisFrame)
        {
            HandleObjClick(_pointer);
            return;
        }

        if (_pointer.press.wasReleasedThisFrame)
        {
            HandleObjRelease(_pointer);
            return;
        }

        if (_pointer.press.isPressed)
        {
            HandleObjDrag(_pointer);
        }
    }

    private void HandleObjClick(Pointer pointer)
    {
        var pointerPos = GetWorldPosition(pointer.position.ReadValue());
        // ReSharper disable once Unity.PreferNonAllocApi
        var hits = Physics2D.OverlapPointAll(pointerPos);

        if (hits is null)
        {
            Logger.Debug?.Log("Player clicked on nothing");
            return;
        }

        var topHit = hits.GetHitOnTop<IClickable>();
        if (topHit is null)
        {
            Logger.Debug?.Log($"Clicked object is not {nameof(IClickable)}");
            return;
        }

        _currSelected = topHit.gameObject;
        _currClickPos = pointerPos;
        Events.Publish(new PointerClickEvt(topHit, pointerPos));
        Logger.Debug?.Log($"Player clicked on {topHit.gameObject}");
    }

    private void HandleObjDrag(Pointer pointer)
    {
        if (_currSelected is null)
        {
            return;
        }

        if (!_currSelected.TryGetComponent<IDraggable>(out _))
        {
            return;
        }

        var pointerPos = GetWorldPosition(pointer.position.ReadValue());

        if (!_isDragging && ShouldStartDragging(pointerPos))
        {
            Logger.Debug?.Log($"Starting to drag object {_currSelected.name}");
            _isDragging = true;
        }

        if (_isDragging)
        {
            Events.Publish(new PointerDragEvt(_currSelected, pointerPos));
        }
    }

    private bool ShouldStartDragging(Vector3 pointerPos)
    {
        if (_currClickPos is null)
        {
            return false;
        }

        var distance = Vector2.Distance(pointerPos, _currClickPos.Value);
        Logger.Debug?.Log($"Checking if drag should start. If distance: {distance}  > {DRAG_DISTANCE_THRESHOLD}");
        return distance > DRAG_DISTANCE_THRESHOLD;
    }

    private void HandleObjRelease(Pointer pointer)
    {
        if (_currClickPos is null || _currSelected is null)
        {
            return;
        }

        var pointerPos = GetWorldPosition(pointer.position.ReadValue());
        Events.Publish(new PointerReleaseEvt(_currSelected, pointerPos, _isDragging));
        _currSelected = null;
        _currClickPos = null;
        _isDragging = false;
    }

    private Vector3 GetWorldPosition(Vector2 pointerPos)
    {
        // NOTE: Garante que o cálculo de mundo considere a distância da camera para um plano oposto à camera
        var posWithDepth = new Vector3(pointerPos.x, pointerPos.y, -GameManager.DepthLayers.CAMERA_GLOBAL_Z);
        return _cam.ScreenToWorldPoint(posWithDepth);
    }

    /// <summary>min distance in world units the mouse should move so the event can be considered a drag</summary>
    private const float DRAG_DISTANCE_THRESHOLD = 0.3f;
}
