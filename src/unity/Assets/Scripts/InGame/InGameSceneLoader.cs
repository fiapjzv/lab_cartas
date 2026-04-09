using Game.Core.Services;
using UnityEngine;

/// <summary>Visualização e distribuição inicial de elementos de um novo jogo</summary>
public class InGameSceneLoader : MonoBehaviour
{
    private IGameLogger _logger = null!;

    [field: SerializeField]
    private GameObject CardPrefab { get; set; } = null!;

    [field: SerializeField]
    private GameObject CardZonePrefab { get; set; } = null!;

    public void Awake()
    {
        _logger = Service.Get<IGameLogger>();

        RenderCardZones();
        RenderPlayerHand();
    }

    private void RenderCardZones()
    {
        _logger.Debug?.Log($"Loading {ZONE_COUNT} card zones: ZoneSpacing = {ZONE_SPACING}; ZoneY = {ZONE_Y}");
        DistributeGameObjects(CardZonePrefab, ZONE_COUNT, ZONE_SPACING, ZONE_Y);
    }

    private void RenderPlayerHand()
    {
        _logger.Debug?.Log($"Loading {CARD_COUNT} cards: CardSpacing = {CARD_SPACING}; CardY = {CARD_Y}");
        DistributeGameObjects(CardPrefab, CARD_COUNT, CARD_SPACING, CARD_Y);
    }

    private static void DistributeGameObjects(GameObject prefab, int count, float spacing, float posY)
    {
        var zoneWidth = prefab.transform.localScale.x;
        var layoutWidth = count * zoneWidth + (count - 1) * spacing;
        var cardZoneCenterStep = zoneWidth + spacing;
        var startX = -layoutWidth / 2f + zoneWidth / 2f;

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
