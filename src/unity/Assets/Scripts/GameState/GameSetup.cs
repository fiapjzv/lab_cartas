using System.Threading.Tasks;
using UnityEngine;

public partial class GameSetup : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamPrefab = null!;

    private async Task Awake()
    {
        DontDestroyOnLoad(gameObject);

        var (scenes, logger) = SetupServices();
        ShowLoading(logger);
        SetupCamera(mainCamPrefab, logger);

        await TestServerConn(logger);

        // NOTE: this should load the menu
        scenes.ChangeTo(Scene.Menu);
    }

    public enum Screen
    {
        Vertical,
        Horizontal,
    }
}
