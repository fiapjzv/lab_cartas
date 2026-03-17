using Game.Core.Services;

public partial class GameSetup : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamPrefab;

    private IEvents events;
    private Game.Core.Services.ILogger logger;

    private async Task Awake()
    {
        SetupServices();
        ShowLoading();
        SetupCamera(mainCamPrefab);

        await ConnectToServer();
    }
}
