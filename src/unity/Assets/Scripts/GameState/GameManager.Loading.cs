using Game.Core.Services;
using UnityEngine.UIElements;

public partial class GameManager
{
    /// <summary>Mostra a splash screen de loading.</summary>
    private void ShowLoading(UIDocument loadingScreen, IGameLogger logger)
    {
        // NOTE: adding the loading screen inside the GameSetup object so it doesn't get destroyed
        _ = Instantiate(loadingScreen, transform);
        logger.Debug?.Log("Loading screen loaded!");
    }
}
