using UnityEngine;
using UnityEngine.InputSystem;

public partial class PointerHandler
{
    void Update()
    {
        var pointer = Pointer.current;
        if (pointer == null)
        {
            _logger.Error?.Log("No pointer found!");
            return;
        }

        if (pointer.press.wasPressedThisFrame)
        {
            HandleObjClick(pointer);
            return;
        }

        if (pointer.press.isPressed)
        {
            HandleObjDrag(pointer);
        }

        if (pointer.press.wasReleasedThisFrame)
        {
            HandleObjRelease(pointer);
        }
    }

    private void HandleObjClick(Pointer pointer)
    {
        var pointerPos = _cam.ScreenToWorldPoint(pointer.position.ReadValue());
        var hit = Physics2D.OverlapPoint(pointerPos);

        if (hit is null)
        {
            _logger.Debug?.Log($"Player clicked on nothing");
            return;
        }

        _logger.Debug?.Log($"Player cliked on {hit.gameObject}");
        if (!hit.HasComponent<IClickable>())
        {
            _logger.Debug?.Log($"Clicked object is not {nameof(IClickable)}");
            return;
        }

        _currClicked = hit.gameObject;
        _events.Publish(new PointerClickEvt(hit, pointerPos));
    }

    private void HandleObjDrag(Pointer pointer)
    {
        if (_currClicked is null)
        {
            return;
        }

        var pointerPos = _cam.ScreenToWorldPoint(pointer.position.ReadValue());
        if (_currClicked.HasComponent<IDraggable>())
        {
            _events.Publish(new PointerDragEvt(_currClicked, pointerPos));
        }
    }

    private void HandleObjRelease(Pointer pointer)
    {
        var pointerPos = _cam.ScreenToWorldPoint(pointer.position.ReadValue());
        _events.Publish(new PointerReleaseEvt(pointerPos));
        _currClicked = null;
    }
}
