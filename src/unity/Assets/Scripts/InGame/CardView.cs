using System;
using System.Collections.Generic;

public partial class CardView : GameBehavior, IDraggable, IClickable
{
    protected override IEnumerable<IDisposable> SubscribeEvents()
    {
        yield return Events.Subscribe<PointerReleaseEvt>(CheckCardMoved);
        yield return Events.Subscribe<PointerDragEvt>(DragCard);
    }
}
