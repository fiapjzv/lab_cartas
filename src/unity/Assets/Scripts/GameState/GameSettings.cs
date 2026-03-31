using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Game/GameSettings")]
public class GameSettings : ScriptableObject
{
    public Camera mainCameraPrefab = null!;
    public UIDocument loadingScreenPrefab = null!;

    public bool MissingFields()
    {
        return loadingScreenPrefab is null || mainCameraPrefab is null;
    }
}
