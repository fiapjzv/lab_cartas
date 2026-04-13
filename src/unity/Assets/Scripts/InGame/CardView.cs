using System;
using System.Collections.Generic;

public partial class CardView : GameBehavior, IDraggable, IClickable
{
    private CardData? _cardData;

    /// <summary>Altera a informação necessária para exibir os dados de uma carta.</summary>
    public void SetCardData(CardData cardData)
    {
        _cardData = cardData;
    }

    protected override IEnumerable<IDisposable> SubscribeEvents()
    {
        yield return Events.Subscribe<PointerReleaseEvt>(CheckCardMoved);
        yield return Events.Subscribe<PointerDragEvt>(DragCard);
    }
}
