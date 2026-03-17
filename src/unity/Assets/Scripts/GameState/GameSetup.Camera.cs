using Game.Core.Services;

public partial class GameSetup
{
    private void SetupCamera(Camera camPrefab, IGameLogger logger)
    {
        var mainCam = Instantiate(camPrefab);
        logger.Debug?.Log("Main camera setup complete!");
    }
}
