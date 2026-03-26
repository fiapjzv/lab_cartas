using UnityEngine;

public static class GameBootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        Debug.Log($"{nameof(GameBootstrap)} running");

        var gameSetup = new GameObject(nameof(GameSetup));
        Object.DontDestroyOnLoad(gameSetup);
        gameSetup.AddComponent<GameSetup>();
    }
}
