using System;
using System.Collections.Generic;
using Game.Core.Utils;
using UnityEngine;
using UnityEngine.UIElements;
using static GameManager;

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
        ValidateAssumptions();
        _uiDocument = GetComponent<UIDocument>();
        SetPresentation();
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

    private static void ValidateAssumptions()
    {
        // PERF: maybe only check this on debug?
        Guard.Assert(() =>
            Camera.main is not null && Camera.main.orthographic
                ? Result.Ok()
                : Result.Err("We expect an orthographic camera!")
        );

        Guard.Assert(() =>
        {
            var cam = Camera.main!;
            // NOTE: orthographicSize is HALF the total height
            var currentHeight = cam.orthographicSize * 2;
            return Mathf.Approximately(currentHeight, VIEWPORT_HEIGHT)
                ? Result.Ok()
                : Result.Err($"Camera height is {currentHeight}, but CardView expects {VIEWPORT_HEIGHT}!");
        });
    }
}
