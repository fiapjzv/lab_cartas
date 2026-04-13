using System;
using System.Collections.Generic;
using UnityEngine;

public class CardView : GameBehavior, IDraggable, IClickable
{
    protected override IEnumerable<IDisposable> SubscribeEvents()
    {
        yield return Events.Subscribe<PointerReleaseEvt>(CheckCardMoved);
    }

    private void CheckCardMoved(PointerReleaseEvt evt)
    {
        if (evt.Target != gameObject)
        {
            return;
        }

        if (evt.WasDragged)
        {
            TryMoveToArea(evt.PointerPos);
        }
        else
        {
            ShowCardDetails();
        }
    }

    private void ShowCardDetails()
    {
        Logger.Debug?.Log("Show card details");
    }

    private void TryMoveToArea(Vector3 pointerPos)
    {
        Logger.Debug?.Log("Try to move to card area");
    }
}
