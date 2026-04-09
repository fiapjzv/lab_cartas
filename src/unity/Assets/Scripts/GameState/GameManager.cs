using System.Threading.Tasks;
using Game.Core.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour
{
    public void Awake()
    {
        var (events, i18n, logger) = SetupServices();

        var gameSettings = Resources.Load<GameSettings>(GAME_SETTINGS_CONFIG_PATH);
        ValidateConfig(gameSettings, logger);

        SubscribeQuitEvent(events, logger);
        StartI18n(i18n, logger);
        ShowLoading(gameSettings.loadingScreenPrefab, logger);
        SetupCamera(gameSettings.mainCameraPrefab, logger);

        _ = DoSetupAsync(events, logger);
    }

    private static void ValidateConfig(GameSettings? gameSettings, IGameLogger logger)
    {
        Guard.NotNull(gameSettings?.loadingScreenPrefab, logger);
        Guard.NotNull(gameSettings?.mainCameraPrefab, logger);
    }

    // NOTE: o ciclo de vida dos componentes unity são fire-and-forget deixando isso explícito aqui
    private async Task DoSetupAsync(IEvents events, IGameLogger logger)
    {
        await TestServerConn(logger);

        var currScene = SceneManager.GetActiveScene().name.AsScene();
        if (currScene == Scene.Bootstrap)
        {
            events.Publish(new ChangeSceneEvt(Scene.MainMenu));
        }
        else
        {
#if DEBUG
            logger.Warn?.Log($"Starting execution on scene {currScene}.");
            events.Publish(new ChangeSceneEvt(currScene));
#else
            Guard.Panic($"Cannot start game on scene {currScene}");
#endif
        }
    }

    private const string GAME_SETTINGS_CONFIG_PATH = "Config/GameSettings";
}
