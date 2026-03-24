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
        }

        private void ResetProgressBar()
        {
            _progressBar.style.width = 0;
        }

        private const string PROGRESS_FILL_UI_ELEM = "progress-fill";
    }
}
