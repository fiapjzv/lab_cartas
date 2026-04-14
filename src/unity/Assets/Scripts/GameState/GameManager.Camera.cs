using Game.Core.Services;
using UnityEngine;

public partial class GameManager
{
    private void SetupCamera(Camera camPrefab, IGameLogger logger)
    {
        var cam = FindAnyObjectByType<Camera>();
        if (cam is not null)
        {
#if !DEBUG
            Guard.Panic("The game cannot start with a camera in Release mode!");
#endif
            logger.Warn?.Log("Game starting with custom camera!");
        }
        else
        {
            // NOTE: adding the camera inside the GameSetup object so it doesn't get destroyed
            cam ??= Instantiate(camPrefab, transform);
        }

        if (!Mathf.Approximately(cam.transform.position.z, DepthLayers.CAMERA_GLOBAL_Z))
        {
            logger.Error?.Log(
                $"We assume that the z position of the main camera must be {nameof(DepthLayers.CAMERA_GLOBAL_Z)}!"
                    + $"Not the actual value of {cam.transform.position.z}"
            );
            return;
        }

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
