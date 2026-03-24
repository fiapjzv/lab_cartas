using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public partial class GameSetup : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamPrefab = null!;

    [SerializeField]
    private UIDocument _loadingScreen = null!;

    private async Task Awake()
    {
        DontDestroyOnLoad(gameObject);

        var (scenes, logger) = SetupServices();
        ShowLoading(logger);
        SetupCamera(mainCamPrefab, logger);

        await TestServerConn(logger);

        // NOTE: this should load the menu
        await scenes.ChangeTo(Scene.MainMenu);
    }
}
