using System;
using UnityEngine;
using static CardZoneView.Type;

public partial class CardZoneView
{
    private void SetupCollider()
    {
        var viewportSize = ViewPort.SizeInWorldUnits();
        _boxCollider = gameObject.AddComponent<BoxCollider2D>();
        _boxCollider.offset = Vector2.zero;

        Logger.Debug?.Log($"Setting up in game zone {ZoneType} for a viewport of {(viewportSize.x, viewportSize.y)}");
        // NOTE: Using transform.localPosition for the center positions (these zones are children of a UI/Game canvas)
        (_boxCollider.size, transform.localPosition) = ZoneType switch
        {
            PlayerHand => (viewportSize * PLAYER_HAND_SCREEN_RATIO, PLAYER_HAND_CENTER_POS),
            PlayerDeck => (viewportSize * DECK_SCREEN_RATIO, DECK_CENTER_POS),
            InGameArea => (viewportSize * IN_GAME_SCREEN_RATIO, IN_GAME_CENTER_POS),
            Graveyard => (viewportSize * GRAVEYARD_SCREEN_RATIO, GRAVEYARD_CENTER_POS),
            _ => throw new ArgumentOutOfRangeException(nameof(ZoneType), ZoneType.ToString()),
        };
    }

    // ReSharper disable InconsistentNaming
    // NOTE: adding 5% more height ratio
    private static readonly Vector2 PLAYER_HAND_SCREEN_RATIO = new(
        0.7f,
        CardView.SMALL_CARD_SCREEN_RATIO_HEIGHT + 0.02f
    );
    private static readonly Vector2 IN_GAME_SCREEN_RATIO = new(0.8f, 0.7f);
    private static readonly Vector2 DECK_SCREEN_RATIO = new(0.15f, 0.2f);
    private static readonly Vector2 GRAVEYARD_SCREEN_RATIO = new(0.1f, 0.15f);

    private static readonly Vector2 PLAYER_HAND_CENTER_POS = new(0f, -7f);
    private static readonly Vector2 IN_GAME_CENTER_POS = new(0f, 0f);
    private static readonly Vector2 DECK_CENTER_POS = new(-10f, -5f);
    private static readonly Vector2 GRAVEYARD_CENTER_POS = new(
        // NOTE: aligning with the left side of the deck area (will need to recalculate manually if it changes its size)
        x: -10.61f,
        y: -2f
    );
    // ReSharper enable InconsistentNaming
}
