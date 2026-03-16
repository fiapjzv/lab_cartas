using Game.Core.Services;

public partial class GameSetup : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamPrefab;

    private IEvents events;

    private async Task Awake()
    {
        SetupServices();
        ShowLoading();
        SetupCamera(mainCamPrefab);

        await ConnectToServer();
    }
}
