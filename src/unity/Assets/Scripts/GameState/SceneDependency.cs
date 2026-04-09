using System;
using System.Threading.Tasks;

public interface ISceneDependency
{
    float LoadWeight { get; }
    Task Load(Action<ISceneDependency, float> informProgress);
}

public static partial class SceneDependency
{
    public static ISceneDependency[] Dependencies(this Scene scene, UnityEngine.AsyncOperation sceneLoad)
    {
        return scene switch
        {
            Scene.Bootstrap => throw new NotImplementedException(),
            Scene.MainMenu => new[] { new Unity(sceneLoad, 1) },
            Scene.InGame => new[] { new Unity(sceneLoad, 1) },
            Scene.Story => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(scene), scene, null),
        };
    }
}
