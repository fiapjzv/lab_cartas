using System;
using Game.Core.Services;

/// <summary>Serviço de controle de cenas.</summary>
public partial class Scenes
{
    private readonly IGameLogger _logger;
    private readonly IEvents _events;

    private Scene _currScene;
    private Scene _nextScene;

    public Scenes(IEvents events, IGameLogger logger)
    {
        _events = events;
        _logger = logger;

        _events.Subscribe<ChangeSceneEvt>(ChangeTo);
    }
}

public enum Scene
{
    Bootstrap,
    MainMenu,
    InGame,
    Story,
}

public static class SceneExtensions
{
    public static string Name(this Scene scene)
    {
        return scene switch
        {
            Scene.MainMenu => "MainMenuScene",
            Scene.InGame => "InGameScene",
            Scene.Story => "StoryScene",
            Scene.Bootstrap => "BootstrapScene",
            _ => throw new ArgumentOutOfRangeException(nameof(scene), scene, null),
        };
    }

    public static Scene AsScene(this string sceneName)
    {
        return sceneName switch
        {
            "MainMenuScene" => Scene.MainMenu,
            "InGameScene" => Scene.InGame,
            "StoryScene" => Scene.Story,
            "BootstrapScene" => Scene.Bootstrap,
            _ => throw new ArgumentOutOfRangeException(nameof(sceneName), sceneName, null),
        };
    }
}
