using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UI
{
    public partial class LoadingScreen
    {
        private VisualElement _spinner = null!;
        private IVisualElementScheduledItem? _spinnerRotateTask;
        private float _spinnerCurrAngle;

        private void EnsureSpinnerElement(VisualElement root)
        {
            _spinner = Guard.ElementIsPresent(root, SNIPPER_UI_ELEM, _logger);
            _spinner.style.display = DisplayStyle.None;
        }

        private void ShowSpinner()
        {
            if (_spinnerRotateTask is not null)
            {
                _logger.Warn?.Log("Spinner already active");
                return;
            }

            var frameBudgetMs = GameManager.FrameBudgetInMs();
            _spinnerRotateTask = _spinner.schedule.Execute(RotateSpinner).Every(frameBudgetMs);
            _spinner.style.display = DisplayStyle.Flex;
        }

        private void HideSpinner()
        {
            if (!gameObject.activeSelf || _spinnerRotateTask is null)
            {
                _logger.Warn?.Log("Spinner already innactive");
            }

            _spinnerRotateTask?.Pause();
            _spinnerRotateTask = null;
            _spinner.style.display = DisplayStyle.None;
        }

        private void RotateSpinner()
        {
            _spinnerCurrAngle += 180f * Time.deltaTime;
            _spinner.style.rotate = Quaternion.Euler(0, 0, _spinnerCurrAngle);
        }

        private const string SNIPPER_UI_ELEM = "spinner";
    }
}
