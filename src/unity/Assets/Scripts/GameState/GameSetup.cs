using System.Threading.Tasks;
using Game.Core.Services;
using UnityEngine;
using UnityEngine.UIElements;

public partial class GameSetup : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamPrefab = null!;

    [SerializeField]
    private UIDocument _loadingScreen = null!;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        var (scenes, logger) = SetupServices();
        ShowLoading(logger);
        SetupCamera(mainCamPrefab, logger);

        _ = DoSetupAsync(scenes, logger);
    }

    // NOTE: o ciclo de vida dos componentes unity são fire-and-forget deixando isso explícito aqui
    private async Task DoSetupAsync(IScenes scenes, IGameLogger logger)
    {
        await TestServerConn(logger);
        await scenes.ChangeTo(Scene.MainMenu);
    }
}
