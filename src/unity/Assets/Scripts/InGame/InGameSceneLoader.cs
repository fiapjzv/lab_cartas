using Game.Core.Services;
using UnityEngine;

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
        DistributeGameObjects(CardZonePrefab, ZONE_COUNT, ZONE_SPACING, ZONE_Y);
    }

    private void RenderPlayerHand()
    {
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

    const int ZONE_COUNT = 8;
    const float ZONE_Y = -2.0f;
    const float ZONE_SPACING = 0.3f;

    const int CARD_COUNT = 5;
    const float CARD_Y = -5.0f;
    const float CARD_SPACING = 0.12f;
}
