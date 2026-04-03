using System.Collections.Generic;

public partial class Scenes
{
    private float _totalProgress;
    private readonly Dictionary<ISceneDependency, float> _currLoadingProgress = new();

    private void IncrementProgress(ISceneDependency dependency, float newDepProgress)
    {
        if (!SafeGetCurrPogress(dependency, out var currDepProgress))
        {
            _logger.Error?.Log($"Trying to increment invalid scene dependency {dependency}");
            return;
        }

        var incr = newDepProgress - currDepProgress;
        if (incr < 0)
        {
            // NOTE: protecting against decrement
            return;
        }

        var (scene, totalPogress) = SafeSetCurrProgress(dependency, newDepProgress);
        _events.Publish(new SceneLoadProgressEvt(scene, totalPogress));
    }

    private (Scene, float) SafeSetCurrProgress(ISceneDependency dependency, float newDepProgress)
    {
        lock (_lock)
        {
            _currLoadingProgress[dependency] = newDepProgress;
            _totalProgress += newDepProgress * dependency.LoadWeight;
            return (_nextScene, _totalProgress);
        }
    }

    private bool SafeGetCurrPogress(ISceneDependency dependency, out float currDepProgress)
    {
        lock (_lock)
        {
            if (!_currLoadingProgress.TryGetValue(dependency, out currDepProgress))
            {
                return false;
            }
        }
        return true;
    }

    private void SafeResetProgress(ISceneDependency[] dependencies)
    {
        lock (_lock)
        {
            _currLoadingProgress.Clear();
            foreach (var dep in dependencies)
            {
                _currLoadingProgress[dep] = 0;
            }
            _totalProgress = 0;
        }
    }
}
