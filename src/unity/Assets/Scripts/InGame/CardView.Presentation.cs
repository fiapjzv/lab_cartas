using System;
using UnityEngine;

public partial class CardView
{
    private void SetUIViewTree()
    {
        switch (CurrentState)
        {
            case State.SMALL:
                Logger.Debug?.Log("SMALL");
                gameObject.transform.localScale = new Vector3(SMALL_CARD_SCREEN_SIZE, SMALL_CARD_SCREEN_SIZE);
                _uiDocument.visualTreeAsset = CardSmallVT;
                // _boxCollider.transform.localScale = SMALL_CARD_SCALE;
                break;
            case State.DETAILS:
                gameObject.transform.localScale = new Vector3(DETAILS_CARD_SCREEN_SIZE, DETAILS_CARD_SCREEN_SIZE);
                _uiDocument.visualTreeAsset = CardDetailsVT;
                // _boxCollider.transform.localScale = DETAILS_CARD_SCALE;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(CurrentState), CurrentState.ToString());
        }
    }

    // NOTE: the current screen view port has 14 world units of height
    //       using an aspect of 10x14 (with 100 ppu) the default card will would occupy the entire screen
    //       these multipliers will ensure:
    //          - small card occupying 35% of the screen
    //          - detailed card occupying 90% of the screen
    private const float SMALL_CARD_SCREEN_SIZE = 0.35f;
    private const float DETAILS_CARD_SCREEN_SIZE = 0.9f;
}
