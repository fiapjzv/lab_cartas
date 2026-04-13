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

        if (_pointer.press.isPressed)
        {
            HandleObjDrag(_pointer);
        }

        if (_pointer.press.wasReleasedThisFrame)
        {
            HandleObjRelease(_pointer);
        }
    }

    private void HandleObjClick(Pointer pointer)
    {
        var pointerPos = GetWorldPosition(pointer.position.ReadValue());
        var hit = Physics2D.OverlapPoint(pointerPos);

        if (hit is null)
        {
            Logger.Debug?.Log("Player clicked on nothing");
            return;
        }

        if (!hit.HasComponent<IClickable>())
        {
            Logger.Debug?.Log($"Clicked object is not {nameof(IClickable)}");
            return;
        }

        Logger.Debug?.Log($"Player clicked on {hit.gameObject}");
        _currSelected = hit.gameObject;
        Events.Publish(new PointerClickEvt(hit, pointerPos));
    }

    private void HandleObjDrag(Pointer pointer)
    {
        if (_currSelected is null)
        {
            return;
        }

        var pointerPos = GetWorldPosition(pointer.position.ReadValue());
        if (_currSelected.HasComponent<IDraggable>())
        {
            Events.Publish(new PointerDragEvt(_currSelected, pointerPos));
        }
    }

    private void HandleObjRelease(Pointer pointer)
    {
        var pointerPos = GetWorldPosition(pointer.position.ReadValue());
        Events.Publish(new PointerReleaseEvt(pointerPos));
        _currSelected = null;
    }

    private Vector3 GetWorldPosition(Vector2 pointerPos)
    {
        // NOTE: Garante que o cálculo de mundo considere a distância da camera para um plano oposto à camera
        var posWithDepth = new Vector3(pointerPos.x, pointerPos.y, -GameManager.DepthLayers.CAMERA_GLOBAL_Z);
        return _cam.ScreenToWorldPoint(posWithDepth);
    }
}
