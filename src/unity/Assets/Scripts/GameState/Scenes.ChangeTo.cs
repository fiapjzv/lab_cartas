using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public partial class Scenes
{
    private bool _isLoading;
    private readonly object _lock = new();

    // <inheritdoc cref="IScenes.Change" />
    public async Task ChangeTo(Scene scene)
    {
        if (_currScene == scene)
        {
            _logger.Warn?.Log($"Scene {scene} already loaded");
            return;
        }

        var sceneName = GetSceneName(scene);
        if (!IsValid(sceneName))
        {
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
        }

        try
        {
            await DoChangeTo(scene, sceneName);
        }
        finally
        {
            lock (_lock)
            {
                _isLoading = false;
            }
        }
    }

    private async Task DoChangeTo(Scene scene, string sceneName)
    {
        _logger.Info?.Log($"Changing to scene: {sceneName}");

        // SAFETY: using local variable here to ensure valid reference
        var loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingSceneOperation.allowSceneActivation = false;

        _events.Publish(new SceneLoadStartEvt(scene));
        while (loadingSceneOperation.progress < MAX_PROGRESS)
        {
            _events.Publish(new SceneLoadProgressEvt(scene, loadingSceneOperation.progress));
            await 50.Millis().Delay();
        }

        _events.Publish(new SceneLoadCompleteEvt(scene));
        _currScene = scene;

        // NOTE: allowing unity to actually change the scene but giving some breath time to animations
        await 100.Millis().Delay();
        loadingSceneOperation.allowSceneActivation = false;
    }

    private static string GetSceneName(Scene scene)
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

    // NOTE: unity decided that progress only goes until 0.9
    //       using 0.89 because of floating number errors 🫠
    private const float MAX_PROGRESS = 0.89f;
}
