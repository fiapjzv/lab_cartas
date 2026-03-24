using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UI
{
    public partial class LoadingScreen
    {
        private VisualElement _spinner = null!;
        private IVisualElementScheduledItem? _task;
        private float _spinnerCurrAngle;

        private void EnsureSpinnerElement(VisualElement root)
        {
            _spinner = Guard.ElementIsPresent(root, SNIPPER_UI_ELEM, _logger);
            _spinner.style.display = DisplayStyle.None;
        }

        private void ShowSpinner()
        {
            if (_task is not null)
            {
                _logger.Warn?.Log("Spinner already active");
                return;
            }

            var fameBudgetMs = GameSetup.FrameBudgetInMs();
            _task = _spinner.schedule.Execute(RotateSpinner).Every(fameBudgetMs);
            _spinner.style.display = DisplayStyle.Flex;
        }

        private void HideSpinner()
        {
            if (!gameObject.activeSelf || _task is null)
            {
                _logger.Warn?.Log("Spinner already innactive");
            }

            _task?.Pause();
            _task = null;
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
