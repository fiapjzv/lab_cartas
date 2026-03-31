using System;
using System.Threading.Tasks;
using Game.Core.Services;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    private void Awake()
    {
        var setupConfig = Resources.Load<GameSettings>(GAME_SETUP_CONFIG_PATH);
        ValidateConfig(setupConfig);

        var (scenes, logger) = SetupServices();
        ShowLoading(setupConfig.loadingScreenPrefab, logger);
        SetupCamera(setupConfig.mainCameraPrefab, logger);

        _ = DoSetupAsync(scenes, logger);
    }

    private void ValidateConfig(GameSettings? setupConfig)
    {
        var error =
            setupConfig is null ? $"No {nameof(GameSettings)} is available on Resources "
            : setupConfig.MissingFields() ? $"{nameof(GameSettings)} is missing fields"
            : null;

        if (error is not null)
        {
            throw new ApplicationException(error);
        }
    }

    // NOTE: o ciclo de vida dos componentes unity são fire-and-forget deixando isso explícito aqui
    private async Task DoSetupAsync(IScenes scenes, IGameLogger logger)
    {
        await TestServerConn(logger);
        await scenes.ChangeTo(Scene.MainMenu);
    }

    private const string GAME_SETUP_CONFIG_PATH = "Config/GameSetupConfig";
}
