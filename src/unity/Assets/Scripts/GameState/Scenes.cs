using System.Threading.Tasks;
using Game.Core.Services;

/// <summary>Serviço de controle de cenas.</summary>
public interface IScenes
{
    /// <summary>
    /// Agenda uma mudança de cena vai acontecer com a publicação do evento <see cref="SceneLoadCompleteEvt">.
    /// </summary>
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
