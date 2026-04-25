using UnityEngine;

public partial class InGameSceneLoader
{
    private void SetupGameField()
    {
        Logger.Debug?.Log($"Loading {ZONE_COUNT} card zones: ZoneSpacing = {ZONE_SPACING}; ZoneY = {ZONE_Y}");
        // DistributeGameObjects(CardZonePrefab, ZONE_COUNT, ZONE_SPACING, ZONE_Y);
    }
}
