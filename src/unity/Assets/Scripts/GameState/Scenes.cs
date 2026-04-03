using System;
using System.Threading.Tasks;
using Game.Core.Services;

/// <summary>Serviço de controle de cenas.</summary>
public interface IScenes
{
    /// <summary>
    /// Agenda uma mudança de cena vai acontecer com a publicação do evento <see cref="SceneLoadCompleteEvt">.
    /// </summary>
    /// <remarks>Antes de carregar a cena garante que as dependencias estejam carregadas.</remarks>
    Task ChangeTo(Scene scene);

    /// <summary>Verifica se essa cena é válida no unity</summary>
    bool IsValid(string scene);
}

// <inheritdoc cref="IScenes" />
public partial class Scenes : IScenes
{
    private readonly IGameLogger _logger;
    private readonly IEvents _events;

    private Scene _currScene;
    private Scene _nextScene;

    public Scenes(IEvents events, IGameLogger logger)
    {
        _events = events;
        _logger = logger;
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
