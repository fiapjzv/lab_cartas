using UnityEngine;

/// <summary>Loads the in-game scene where the cards battle happen.</summary>
public partial class InGameSceneLoader : GameBehavior
{
    [field: SerializeField]
    private GameObject GameFieldPrefab { get; set; } = null!;

    private CardBattleState _battleState;

    // [field: SerializeField]
    // private GameObject SmallCardPrefab { get; set; } = null!;

    // [field: SerializeField]
    // private GameObject CardZonePrefab { get; set; } = null!;

    protected override void Init()
    {
        var playerAccount = Guard.NotNull(Service.Get<IPlayerAccount>(), Logger);
        var playerDeck = Guard.NotNull(Service.Get<IPlayerDecks>().CurrDeck(), Logger);
        var match = Guard.NotNull(Service.Get<IBattleMatchMaker>(), Logger).CurrMatch();
        _battleState = new CardBattleState(playerAccount.Id, playerDeck.Id, match.Sorcerers());
        // RenderPlayerHand();
    }

    // private void RenderPlayerHand()
    // {
    //     Logger.Debug?.Log($"Loading {CARD_COUNT} cards: CardSpacing = {CARD_SPACING}; CardY = {CARD_Y}");
    //     DistributeGameObjects(SmallCardPrefab, CARD_COUNT, CARD_SPACING, CARD_Y);
    // }
    //
    // private void DistributeGameObjects(GameObject prefab, int count, float spacing, float posY)
    // {
    //     var objWidth = prefab.transform.localScale.x;
    //     var layoutWidth = count * objWidth + (count - 1) * spacing;
    //     var cardZoneCenterStep = objWidth + spacing;
    //     var startX = -layoutWidth / 2f + objWidth / 2f;
    //
    //     Logger.Debug?.Log(
    //         $"Starting to distribute {count} objects: ObjWidth={objWidth}, layoutWidth={layoutWidth}, CardZoneCenterStep={cardZoneCenterStep}, startX={startX}"
    //     );
    //     for (var i = 0; i < count; i++)
    //     {
    //         var instance = Instantiate(prefab, gameObject.transform);
    //         instance.transform.position = new Vector3(
    //             x: startX + i * cardZoneCenterStep,
    //             y: posY,
    //             z: GameManager.DepthLayers.TABLE_ZONE_Z
    //         );
    //     }
    // }

    private const int ZONE_COUNT = 8;
    private const float ZONE_Y = -4.0f;
    private const float ZONE_SPACING = 0.3f;

    private const int CARD_COUNT = 5;
    private const float CARD_Y = -7.0f;
    private const float CARD_SPACING = 0.12f;
}
