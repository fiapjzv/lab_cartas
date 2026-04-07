using System;
using System.Threading.Tasks;
using Game.Core.Services;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public void Awake()
    {
        var gameSettings = Resources.Load<GameSettings>(GAME_SETTINGS_CONFIG_PATH);
        ValidateConfig(gameSettings);

        var (events, i18n, logger) = SetupServices();
        SubscribeQuitEvent(events, logger);
        StartI18n(i18n, logger);
        ShowLoading(gameSettings.loadingScreenPrefab, logger);
        SetupCamera(gameSettings.mainCameraPrefab, logger);

        _ = DoSetupAsync(events, logger);
    }

    private void ValidateConfig(GameSettings? gameSettings)
    {
        var error =
            gameSettings is null ? $"No {nameof(GameSettings)} is available on Resources "
            : gameSettings.MissingFields() ? $"{nameof(GameSettings)} is missing fields"
            : null;

        if (error is not null)
        {
            throw new ApplicationException(error);
        }
    }

    // NOTE: o ciclo de vida dos componentes unity são fire-and-forget deixando isso explícito aqui
    private async Task DoSetupAsync(IEvents events, IGameLogger logger)
    {
        await TestServerConn(logger);
        events.Publish(new ChangeSceneEvt(Scene.MainMenu));
    }

    private const string GAME_SETTINGS_CONFIG_PATH = "Config/GameSettings";
}
