using System;
using UnityEngine.UIElements;

namespace Game.UI
{
    public partial class LoadingScreen
    {
        private VisualElement _progressBar = null!;

        private void EnsureLoadingBar(VisualElement root)
        {
            _progressBar = Guard.ElementIsPresent(root, PROGRESS_FILL_UI_ELEM, _logger);
            _progressBar.style.display = DisplayStyle.None;

            _events.Subscribe<SceneLoadingEvt>(IncreaseProgressBar);
            _events.Subscribe<SceneLoadedEvt>(FillProgressBar);
        }

        private void ResetProgressBar()
        {
            _progressBar.style.width = 0;
        }

        private void IncreaseProgressBar(SceneLoadingEvt evt)
        {
            var progressPercent = Length.Percent(evt.Progress);
            _logger.Debug?.Log($"Increasing progress bar to {progressPercent}");
            _progressBar.style.width = progressPercent;
        }

        private void FillProgressBar(SceneLoadedEvt evt)
        {
            _progressBar.style.width = Length.Percent(100);
        }

        private const string PROGRESS_FILL_UI_ELEM = "progress-fill";
    }
}
