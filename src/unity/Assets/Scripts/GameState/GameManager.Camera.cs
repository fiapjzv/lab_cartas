using Game.Core.Services;
using UnityEngine;

public partial class GameManager
{
    private void SetupCamera(Camera camPrefab, IGameLogger logger)
    {
        // NOTE: adding the camera inside the GameSetup object so it doesn't get destroyed
        var cam = Instantiate(camPrefab, transform);

        if (!ColorUtility.TryParseHtmlString(CLEAR_SCREEN_COLOR, out var color))
        {
            logger.Error?.Log(
                $"{nameof(CLEAR_SCREEN_COLOR)} must be a valid RGB color! Invalid value: {CLEAR_SCREEN_COLOR}"
            );
            return;
        }
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = color;

        logger.Debug?.Log("Main camera setup complete!");
    }
}
