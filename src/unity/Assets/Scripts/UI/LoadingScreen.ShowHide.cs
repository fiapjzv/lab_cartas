using UnityEngine.UIElements;

namespace Game.UI
{
    public partial class LoadingScreen
    {
        private void ShowScreen(SceneLoadStartEvt _)
        {
            Logger.Info?.Log("Displaying loading screen");

            _screen.AddToClassList("show");
            StartSpinner();
            StartProgressBar();
            // _root.style.opacity = 1.0f;
            // _root.style.display = DisplayStyle.Flex;
            // _root.pickingMode = PickingMode.Ignore;
        }

        private void HideScreen(LoadingScreen100Percent _)
        {
            // _root.style.opacity = 0f;
            // _root.style.display = DisplayStyle.None;
            // _root.pickingMode = PickingMode.Ignore;

            _screen.RemoveFromClassList("show");
            EndProgressBar();
            StopSpinner();
        }
    }
}
