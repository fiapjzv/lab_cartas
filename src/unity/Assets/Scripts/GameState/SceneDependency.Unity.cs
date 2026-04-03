using System;
using System.Threading.Tasks;

public static partial class SceneDependency
{
    public class Unity : ISceneDependency
    {
        private readonly UnityEngine.AsyncOperation _unityLoad;

        public float LoadWeight { get; }

        public Unity(UnityEngine.AsyncOperation unityLoad, float loadWeight)
        {
            if (loadWeight < 0 || loadWeight > 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(loadWeight),
                    loadWeight,
                    "Expected value between 0-1"
                );
            }

            LoadWeight = loadWeight;
            _unityLoad = unityLoad;
        }

        public async Task Load(Action<ISceneDependency, float> informProgress)
        {
            while (_unityLoad.progress < MAX_PROGRESS)
            {
                informProgress(this, _unityLoad.progress);
                await 50.Millis().Delay();
            }

            informProgress(this, 1f);
        }

        // NOTE: unity decided that progress only goes until 0.9
        //       using 0.89 because of floating number errors 🫠
        private const float MAX_PROGRESS = 0.89f;
    }
}
