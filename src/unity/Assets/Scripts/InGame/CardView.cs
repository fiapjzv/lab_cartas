using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public partial class CardView : GameBehavior, IDraggable, IClickable
{
    [field: SerializeField]
    public State CurrentState { get; private set; }

    [field: SerializeField]
    public VisualTreeAsset CardSmallVT { get; private set; } = null!;

    [field: SerializeField]
    public VisualTreeAsset CardDetailsVT { get; private set; } = null!;

    private CardData? _cardData;
    private UIDocument _uiDocument = null!;
    private BoxCollider2D _boxCollider = null!;

    protected override void Init()
    {
        Premisses.ValidateCamera();
        _uiDocument = GetComponent<UIDocument>();
        SetPresentation();
        Logger.Debug?.Log($"Card instantiate with {_boxCollider.size}");
    }

    protected override IEnumerable<IDisposable> SubscribeEvents()
    {
        yield return Events.Subscribe<PointerReleaseEvt>(CheckCardMoved);
        yield return Events.Subscribe<PointerDragEvt>(DragCard);
    }

    public enum State
    {
        /// <summary>
        /// Small with of the card: only name, aura cost and art displayed.
        /// Used when in hand, on forging decks, on the cards album, etc.
        /// </summary>
        SMALL,

        /// <summary>Card with complete details (including card details text).</summary>
        DETAILS,
    }
}
