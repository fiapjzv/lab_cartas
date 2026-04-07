using System;
using System.Threading.Tasks;
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
    Game,
    Story,
}

public static class SceneExtensions
{
    public static string Name(this Scene scene)
    {
        return scene switch
        {
            Scene.MainMenu => "MainMenuScene",
            Scene.Game => "GameScene",
            Scene.Story => "StoryScene",
            Scene.Bootstrap => "BootstrapScene",
            _ => throw new ArgumentOutOfRangeException(nameof(scene), scene, null),
        };
    }
}
