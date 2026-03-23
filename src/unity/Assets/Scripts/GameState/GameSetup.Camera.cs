using Game.Core.Services;
using UnityEngine;

public partial class GameSetup
{
    private void SetupCamera(Camera camPrefab, IGameLogger logger)
    {
        Instantiate(camPrefab);
        logger.Debug?.Log("Main camera setup complete!");
    }
}
