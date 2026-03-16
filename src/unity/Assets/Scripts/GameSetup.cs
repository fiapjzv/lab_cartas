using System;
using System.Threading.Tasks;
using UnityEngine;

public partial class GameSetup : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamPrefab;

    private async Task Awake()
    {
        SetupServices();
        ShowLoading();
        SetupCamera(mainCamPrefab);

        await ConnectToServer();
    }
}
