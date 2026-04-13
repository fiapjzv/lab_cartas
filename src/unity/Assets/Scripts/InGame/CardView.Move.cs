using UnityEngine;

public partial class CardView
{
    private void CheckCardMoved(PointerReleaseEvt evt)
    {
        if (evt.Target != gameObject)
        {
            return;
        }

        if (evt.WasDragged)
        {
            _dragOffset = null;
            TryMoveToArea(evt.PointerPos);
        }
        else
        {
            ShowDetails();
        }
    }

    private void TryMoveToArea(Vector3 pointerPos)
    {
        Logger.Debug?.Log("Try to move to card area");
    }
}
