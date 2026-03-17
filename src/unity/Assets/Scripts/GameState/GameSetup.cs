public partial class GameSetup : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamPrefab = null!;

    private async Task Awake()
    {
        var logger = SetupServices();
        ShowLoading(logger);
        SetupCamera(mainCamPrefab, logger);

        await ConnectToServer(logger);
    }
}
