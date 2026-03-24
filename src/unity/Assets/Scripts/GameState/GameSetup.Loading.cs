using Game.Core.Services;

public partial class GameSetup
{
    /// <summary>Mostra a splash screen de loading.</summary>
    private void ShowLoading(IGameLogger logger)
    {
        // NOTE: adding the loading screen inside the GameSetup object so it doesn't get destroyed
        var loadingScreen = Instantiate(_loadingScreen, transform);
        logger.Debug?.Log("Loading screen loaded!");
    }
}
