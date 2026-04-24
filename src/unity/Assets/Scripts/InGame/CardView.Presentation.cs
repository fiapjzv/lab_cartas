using System;
using UnityEngine;

public partial class CardView
{
    private void SetPresentation()
    {
        var (sizeFactor, visualTree) = CurrentState switch
        {
            State.SMALL => (SMALL_CARD_SCREEN_FACTOR, CardSmallVT),
            State.DETAILS => (DETAILS_CARD_SCREEN_FACTOR, CardDetailsVT),
            _ => throw new ArgumentOutOfRangeException(nameof(CurrentState), CurrentState.ToString()),
        };

        _uiDocument.visualTreeAsset = visualTree;

        _boxCollider = gameObject.AddComponent<BoxCollider2D>();
        _boxCollider.offset = Vector2.zero;
        _boxCollider.size = new Vector2(WORLD_UNITS_BASE_WIDTH, WORLD_UNITS_BASE_HEIGHT);

        transform.localScale = new Vector3(sizeFactor, sizeFactor, 1);
    }

    // NOTE: the current screen view port has 14 world units of height
    //       using an aspect of 10x14 (with 100 ppu) the default card would occupy the entire screen
    //       these multipliers will ensure:
    //          - small card occupying 35% of the screen
    //          - detailed card occupying 90% of the screen
    private const float SMALL_CARD_SCREEN_FACTOR = 0.35f;
    private const float DETAILS_CARD_SCREEN_FACTOR = 0.9f;
    private const float CARD_ASPECT_RATIO = 5f / 7;

    private const float WORLD_UNITS_BASE_HEIGHT = GameManager.VIEWPORT_HEIGHT;
    private const float WORLD_UNITS_BASE_WIDTH = WORLD_UNITS_BASE_HEIGHT * CARD_ASPECT_RATIO;
}
