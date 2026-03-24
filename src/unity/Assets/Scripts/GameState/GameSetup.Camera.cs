using Game.Core.Services;
using UnityEngine;

public partial class GameSetup
{
    private void SetupCamera(Camera camPrefab, IGameLogger logger)
    {
        // NOTE: adding the camera inside the GameSetup object so it doesn't get destroyed
        Instantiate(camPrefab, transform);
        logger.Debug?.Log("Main camera setup complete!");
    }
}
