using Game.Core.Services;
using UnityEngine;

public partial class GameManager
{
    private Camera _defaultCam = null!;

    private void SetupDefaultCam(Camera camPrefab, IGameLogger logger)
    {
        var cam = Instantiate(camPrefab);
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

        _defaultCam = cam;
        logger.Debug?.Log("Default camera setup complete!");
    }

    private void AttachDefaultCam(IGameLogger logger)
    {
        var sceneCam = FindAnyObjectByType<Camera>();
        if (sceneCam is not null)
        {
#if !DEBUG
            Guard.Panic("The game cannot start with a camera in Release mode!");
#endif
            logger.Warn?.Log("Game starting with custom camera!");
        }
        else
        {
            logger.Debug?.Log("Using default camera");
            sceneCam = _defaultCam;
        }

        // NOTE: adding the camera inside the GameManager object so it doesn't get destroyed
        sceneCam.transform.SetParent(transform);
    }
}
