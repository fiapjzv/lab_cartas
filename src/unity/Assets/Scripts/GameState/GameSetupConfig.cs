using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Game/GameSetupConfig")]
public class GameSetupConfig : ScriptableObject
{
    public Camera mainCameraPrefab = null!;
    public UIDocument loadingScreenPrefab = null!;

    public bool MissingFields()
    {
        return loadingScreenPrefab is null || mainCameraPrefab is null;
    }
}
