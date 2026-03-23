using UnityEngine.SceneManagement;

/// <summary>Serviço de controle de cenas.</summary>
public interface IScenes
{
    /// <summary>
    /// Agenda uma mudança de cena vai acontecer com a publicação do evento <see cref="SceneLoadedEvt">.
    /// </summary>
    void ChangeTo(Scene scene);

    /// <summary>Verifica se essa cena é válida no unity</summary>
    bool IsValid(string scene);
}

// <inheritdoc cref="IScenes" />
public class Scenes : IScenes
{
    private readonly IGameLogger _logger;
    private readonly IEvents _events;

    private Scene _currScene;
    private Scene? _loadingScene;
    private AsyncOperation? _loadingSceneOperation;

    public Scenes(IEvents events, IGameLogger logger)
    {
        _events = events;
        _logger = logger;
    }

    // <inheritdoc cref="IScenes.Change" />
    public void ChangeTo(Scene scene)
    {
        if (_currScene == scene)
        {
            _logger.Warn?.Log($"Scene {scene} already loaded");
            return;
        }

        // NOTE: protege contra cliques rápidos em um botão (click spam)
        if (_loadingSceneOperation is not null)
        {
            _logger.Warn?.Log("Scene change already in progress");
            return;
        }

        var sceneName = GetSceneName(scene);
        if (!IsValid(sceneName))
        {
            return;
        }

        _logger.Info?.Log($"Changing to scene: {sceneName}");

        _loadingScene = scene;
        _loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        _loadingSceneOperation.completed += SceneLoadComplete;
        // TODO: timer to publish events for scene loading progress

        _events.Publish(new SceneLoadingEvt { Scene = scene, Progress = 0 });
    }

    private void SceneLoadComplete(AsyncOperation operation)
    {
        if (_loadingScene is null)
        {
            _logger.Error?.Log("No scene to be loaded!");
            return;
        }

        _currScene = _loadingScene.Value;
        _loadingSceneOperation = null;
        _events.Publish(new SceneLoadedEvt { Scene = _currScene });
    }

    /// <inheritdoc cref="IScenes.IsValid" />
    public bool IsValid(string sceneName)
    {
        if (sceneName == "BootstrapScene")
        {
            _logger.Error?.Log($"Should not load BootstrapScene");
            return false;
        }
        var isValid = Application.CanStreamedLevelBeLoaded(sceneName);
        if (isValid)
        {
            return true;
        }
        _logger.Error?.Log($"Scene '{sceneName}' is not in build settings");
        return false;
    }

    private static string GetSceneName(Scene scene)
    {
        return scene switch
        {
            Scene.Menu => "MenuScene",
            Scene.Game => "GameScene",
            Scene.Story => "StoryScene",
            Scene.Bootstrap => "BootstrapScene",
            _ => throw new ArgumentOutOfRangeException(nameof(scene), scene, null),
        };
    }
}

public readonly struct SceneLoadedEvt
{
    public Scene Scene { get; init; }
}

public readonly struct SceneLoadingEvt
{
    public Scene Scene { get; init; }
    public float Progress { get; init; }
}

public enum Scene
{
    Bootstrap,
    Menu,
    Game,
    Story,
}
