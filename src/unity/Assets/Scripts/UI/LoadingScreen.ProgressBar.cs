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

            _events.Subscribe<SceneLoadProgressEvt>(IncreaseProgressBar);
            _events.Subscribe<SceneLoadCompleteEvt>(FillProgressBar);
        }

        private void ResetProgressBar()
        {
            _logger.Debug?.Log("Resetting loading progress bar.");
            _progressBar.style.width = 0;
            _progressBar.style.display = DisplayStyle.Flex;
        }

        private void IncreaseProgressBar(SceneLoadProgressEvt evt)
        {
            DoIncreaseProgressBar(Length.Percent(evt.Progress * 100));
        }

        private void FillProgressBar(SceneLoadCompleteEvt evt)
        {
            DoIncreaseProgressBar(Length.Percent(100));
        }

        private void DoIncreaseProgressBar(Length progressPercent)
        {
            _logger.Debug?.Log($"Increasing progress bar to {progressPercent}");
            _progressBar.style.width = progressPercent;
        }

        private const string PROGRESS_FILL_UI_ELEM = "progress-fill";
    }
}
