using UnityEngine;

/// <summary>Visualização e distribuição inicial de elementos de um novo jogo</summary>
public class InGameSceneLoader : GameBehavior
{
    [field: SerializeField]
    private GameObject SmallCardPrefab { get; set; } = null!;
        
    [field: SerializeField]
    private GameObject CardZonePrefab { get; set; } = null!;

    protected override void Init()
    {
        RenderCardZones();
        RenderPlayerHand();
    }

    private void RenderCardZones()
    {
        Logger.Debug?.Log($"Loading {ZONE_COUNT} card zones: ZoneSpacing = {ZONE_SPACING}; ZoneY = {ZONE_Y}");
        DistributeGameObjects(CardZonePrefab, ZONE_COUNT, ZONE_SPACING, ZONE_Y);
    }

    private void RenderPlayerHand()
    {
        Logger.Debug?.Log($"Loading {CARD_COUNT} cards: CardSpacing = {CARD_SPACING}; CardY = {CARD_Y}");
        DistributeGameObjects(SmallCardPrefab, CARD_COUNT, CARD_SPACING, CARD_Y);
    }

    private void DistributeGameObjects(GameObject prefab, int count, float spacing, float posY)
    {
        var objWidth = prefab.transform.localScale.x;
        var layoutWidth = count * objWidth + (count - 1) * spacing;
        var cardZoneCenterStep = objWidth + spacing;
        var startX = -layoutWidth / 2f + objWidth / 2f;

        Logger.Debug?.Log($"Starting to distribute {count} objects: ObjWidth={objWidth}, layoutWidth={layoutWidth}, CardZoneCenterStep={cardZoneCenterStep}, startX={startX}");
        for (var i = 0; i < count; i++)
        {
            var pos = new Vector3(startX + i * cardZoneCenterStep, posY, GameManager.DepthLayers.TABLE_ZONE_Z);
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }

    private const int ZONE_COUNT = 8;
    private const float ZONE_Y = -4.0f;
    private const float ZONE_SPACING = 0.3f;

    private const int CARD_COUNT = 5;
    private const float CARD_Y = -7.0f;
    private const float CARD_SPACING = 0.12f;
}
