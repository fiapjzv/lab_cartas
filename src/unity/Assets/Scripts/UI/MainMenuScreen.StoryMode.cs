using UnityEngine.UIElements;

namespace Game.UI
{
    public partial class MainMenuScreen
    {
        private void BindStoryModeButton(VisualElement root)
        {
            var quitButton = Guard.ElementIsPresent<Button>(root, "story-btn", _logger);
            quitButton.RegisterCallback<ClickEvent>(PressStoryModeButton);
        }

        private void PressStoryModeButton(ClickEvent _)
        {
            _logger.Debug?.Log("Story mode button clicked");
            _events.Publish(new ChangeSceneEvt(Scene.InGame));
        }
    }
}
