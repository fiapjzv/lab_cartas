using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UI
{
    public partial class LoadingScreen
    {
        private VisualElement _progressBar = null!;
        private IVisualElementScheduledItem _progressBarTask = null!;

        private Length _currProgressPercent;
        private Length _startProgressPercent;
        private Length _targetProgressPercent;
        private float _progressBarElapsedMs;

        private void EnsureLoadingBar(VisualElement root)
        {
            _progressBar = Guard.ElementIsPresent(root, PROGRESS_FILL_UI_ELEM, _logger);
            _progressBar.style.display = DisplayStyle.None;

            var frameBudgetMs = GameSetup.FrameBudgetInMs();
            _progressBarTask = _progressBar
                .schedule.Execute(ProgressBarAnimationFrame)
                .Every(frameBudgetMs);
            _progressBarTask.Pause();

            _events.Subscribe<SceneLoadProgressEvt>(IncreaseProgressBar);
            _events.Subscribe<SceneLoadCompleteEvt>(FillProgressBar);
        }

        private void ResetProgressBar()
        {
            _logger.Debug?.Log("Resetting loading progress bar.");
            _startProgressPercent = Length.Percent(0);
            _targetProgressPercent = Length.Percent(0);
            _progressBarElapsedMs = 0;
            _progressBar.style.width = _startProgressPercent;
            _progressBar.style.display = DisplayStyle.Flex;
        }

        private void IncreaseProgressBar(SceneLoadProgressEvt evt)
        {
            IncreaseTargetProgress(Length.Percent(evt.Progress * 100));
        }

        private void FillProgressBar(SceneLoadCompleteEvt evt)
        {
            IncreaseTargetProgress(Length.Percent(100));
        }

        private void IncreaseTargetProgress(Length progressPercent)
        {
            _logger.Debug?.Log($"Increasing progress bar to {progressPercent}");
            _targetProgressPercent = progressPercent;
            if (!_progressBarTask.isActive)
            {
                _logger.Debug?.Log("Resuming progress bar animation!");
                _progressBarTask.Resume();
            }
        }

        private void ProgressBarAnimationFrame()
        {
            _progressBarElapsedMs += Time.deltaTime;
            var t = Mathf.Clamp01(_progressBarElapsedMs / MIN_PROGRESS_DURATION_MS);

            var currProgressPercent = Mathf.Lerp(
                _startProgressPercent.value,
                _targetProgressPercent.value,
                t
            );
            _currProgressPercent = Length.Percent(currProgressPercent);
            _logger.Debug?.Log($"Updating progress bar width {_currProgressPercent}");
            _progressBar.style.width = _currProgressPercent;

            if (t >= 1f)
            {
                ProgressBarAnimationEnd();
                return;
            }
        }

        private void ProgressBarAnimationEnd()
        {
            _logger.Debug?.Log("Stopping progress bar animation");
            _progressBarTask.Pause();
            _startProgressPercent = _currProgressPercent;
            _currProgressPercent = _targetProgressPercent;
        }

        private const string PROGRESS_FILL_UI_ELEM = "progress-fill";
    }
}
