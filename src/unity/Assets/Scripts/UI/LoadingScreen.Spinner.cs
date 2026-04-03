using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UI
{
    public partial class LoadingScreen
    {
        private VisualElement _spinner = null!;
        private IVisualElementScheduledItem? _spinnerRotateTask;
        private float _spinnerCurrAngle;

        private void StartSpinner()
        {
            if (_spinnerRotateTask is not null)
            {
                _logger.Error?.Log("Spinner already active");
                return;
            }

            _logger.Debug?.Log("Starting spinner");
            var frameBudgetMs = GameManager.FrameBudgetInMs();
            _spinnerRotateTask = _spinner.schedule.Execute(RotateSpinner).Every(frameBudgetMs);
        }

        private void StopSpinner()
        {
            _logger.Debug?.Log("Stopping spinner");
            _spinnerRotateTask?.Pause();
            _spinnerRotateTask = null;
        }

        private void RotateSpinner()
        {
            _spinnerCurrAngle += 180f * Time.deltaTime;
            _spinner.style.rotate = Quaternion.Euler(0, 0, _spinnerCurrAngle);
        }

        private const string SNIPPER_UI_ELEM = "spinner";
    }
}
