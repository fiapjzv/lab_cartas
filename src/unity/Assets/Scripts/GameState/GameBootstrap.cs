using UnityEngine;

public static class GameBootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        Debug.Log($"{nameof(GameBootstrap)} running");

        var gameSetup = new GameObject(nameof(GameManager));
        Object.DontDestroyOnLoad(gameSetup);
        gameSetup.AddComponent<GameManager>();
    }
}
