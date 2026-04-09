using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class Scenes
{
    private bool _isLoading;
    private readonly object _lock = new();

    /// <summary>
    /// Agenda uma mudança de cena vai acontecer com a publicação do evento <see cref="SceneLoadCompleteEvt">.
    /// </summary>
    /// <remarks>Antes de carregar a cena garante que as dependencias estejam carregadas.</remarks>
    private async Task ChangeTo(ChangeSceneEvt changeSceneEvt)
    {
        var scene = changeSceneEvt.Scene;
        if (_currScene == scene)
        {
            _logger.Warn?.Log($"Scene {scene} already loaded");
            return;
        }

        var sceneName = scene.Name();
        if (!IsValid(sceneName))
        {
            return;
        }

        var currenUnityScene = SceneManager.GetActiveScene().name;
        if (currenUnityScene == sceneName)
        {
            _logger.Warn?.Log("Desired scene already loaded in Unity.");
            return;
        }

        lock (_lock)
        {
            if (_isLoading)
            {
                _logger.Warn?.Log("Scene change already in progress");
                return;
            }
            _isLoading = true;
            _nextScene = scene;
        }

        // SAFETY: using local variable here to ensure valid reference
        var sceneLoad = EnsureUnitySceneLoad(sceneName);
        var dependencies = scene.Dependencies(sceneLoad);
# if DEBUG
        // note: ensuring load progress sums 100% in DEBUG
        var totalWeight = dependencies.Sum(d => d.LoadWeight);
        if (!Mathf.Approximately(totalWeight, 1f))
        {
            var error = $"Total scene dependency weight must be 1.0 (100%)! Current sum {totalWeight}";
            _logger.Error?.Log(error);
            throw new InvalidOperationException(error);
        }
# endif

        try
        {
            SafeResetProgress(dependencies);
            _logger.Info?.Log($"Changing to scene {sceneName} from {currenUnityScene}");

            _events.Publish(new SceneLoadStartEvt(scene));
            await Task.WhenAll(dependencies.Select(LoadDependency));
            _events.Publish(new SceneLoadCompleteEvt(scene, sceneLoad));
        }
        finally
        {
            lock (_lock)
            {
                _isLoading = false;
                _currScene = scene;
            }
        }
    }

    private async Task LoadDependency(ISceneDependency dependency)
    {
        var sw = Stopwatch.StartNew();
        _logger.Info?.Log($"Loading dependency {dependency}");
        await dependency.Load(IncrementProgress);
        _logger.Info?.Log($"Dependency {dependency} LOADED in {sw.ElapsedMilliseconds:N2}ms");
    }

    private AsyncOperation EnsureUnitySceneLoad(string sceneName)
    {
        var currScene = SceneManager.GetActiveScene();
        var currSceneName = currScene.name;

        var sceneLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive)!;
        sceneLoad.allowSceneActivation = false;

        if (sceneName == currSceneName)
        {
            _logger.Warn?.Log($"Reloading the same scene {sceneName}. This is probably a debug scenario.");
            return sceneLoad;
        }

        // NOTE: when the next scene is loaded we will unload the previous one
        sceneLoad.completed += _ => UnloadPreviousScene(currSceneName);
        return sceneLoad;
    }

    private void UnloadPreviousScene(string lastScene)
    {
        _logger.Debug?.Log($"Unloading scene {lastScene}");
        var unload = SceneManager.UnloadSceneAsync(lastScene);
        if (unload is null)
        {
            _logger.Error?.Log($"Failed to unload scene: {lastScene}");
            return;
        }
        unload.completed += _ => CheckIfSceneUnloaded(lastScene);
    }

    private void CheckIfSceneUnloaded(string sceneToUnload)
    {
        var scene = SceneManager.GetSceneByName(sceneToUnload);
        if (scene.IsValid() && scene.isLoaded)
        {
            _logger.Error?.Log($"Failed to unload scene: {sceneToUnload}");
        }
        else
        {
            _logger.Debug?.Log($"Scene unloaded: {sceneToUnload}");
        }
    }
}
