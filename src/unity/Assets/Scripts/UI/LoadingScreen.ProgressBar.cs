using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UI
{
    public partial class LoadingScreen
    {
        private VisualElement _progressBar = null!;
        private IVisualElementScheduledItem? _progressBarTask;

        private Length _currProgressPercent;
        private Length _targetProgressPercent;
        private float _progressBarElapsedMs;

        private void StartProgressBar()
        {
            _logger.Debug?.Log("Starting progress bar.");
            _targetProgressPercent = Length.Percent(0);
            _progressBarElapsedMs = 0;
            _progressBar.style.width = Length.Percent(0);

            var frameBudgetMs = GameManager.FrameBudgetInMs();
            _progressBarTask = _progressBar
                .schedule.Execute(ProgressBarAnimationFrame)
                .Every(frameBudgetMs);
        }

        private void EndProgressBar()
        {
            _logger.Debug?.Log("Stopping progress bar animation");
            _progressBarTask?.Pause();
            _progressBarTask = null;
        }

        private void IncreaseProgressBar(SceneLoadProgressEvt evt) =>
            IncreaseTargetProgress(Length.Percent(evt.Progress * 100));

        private void CompleteProgressBar(SceneLoadCompleteEvt evt)
        {
            _sceneLoadComplete = evt;
            IncreaseTargetProgress(Length.Percent(100));
        }

        private void IncreaseTargetProgress(Length progressPercent)
        {
            if (_progressBarTask is null)
            {
                _logger.Error?.Log("Cannot increase target progress. Progress bar hidden.");
                return;
            }

            _logger.Debug?.Log($"Increasing progress bar to {progressPercent}");
            _targetProgressPercent = progressPercent;

            // _logger.Debug?.Log("Resuming progress bar animation!");
            // _progressBarTask = S
        }

        private void ProgressBarAnimationFrame()
        {
            _progressBarElapsedMs += Time.deltaTime;
            var t = Mathf.Clamp01(_progressBarElapsedMs / MIN_PROGRESS_DURATION_MS);

            var currProgressPercent = Mathf.Lerp(0, _targetProgressPercent.value, t);
            _currProgressPercent = Length.Percent(currProgressPercent);
            _logger.Debug?.Log($"Updating progress bar width {_currProgressPercent}");
            _progressBar.style.width = _currProgressPercent;

            if (t >= 1f)
            {
                _events.Publish(new LoadingScreen100Percent());
            }
        }

        private const string PROGRESS_FILL_UI_ELEM = "progress-fill";

        /// <summary>Tempo mínimo na página de loading (para garantir animações).</summary>
        private const float MIN_PROGRESS_DURATION_MS = 0.3f;
    }
}
