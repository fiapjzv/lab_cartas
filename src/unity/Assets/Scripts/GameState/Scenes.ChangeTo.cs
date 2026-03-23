using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class Scenes
{
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

        _events.Publish(new SceneLoadingEvt(scene, progress: 0));
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
        _events.Publish(new SceneLoadedEvt(_currScene));
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
