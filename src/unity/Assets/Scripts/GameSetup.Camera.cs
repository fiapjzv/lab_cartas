using UnityEngine;

public partial class GameSetup
{
    private Camera mainCam;

    private void SetupCamera(Camera camPrefab)
    {
        mainCam = Instantiate(camPrefab);
        Debug.Log("Main camera setup complete!");
    }
}
