using UnityEngine.UIElements;

namespace Game.UI
{
    public partial class MainMenuScreen
    {
        private void BindStoryModeButton(VisualElement root)
        {
            var quitButton = Guard.ElementIsPresent<Button>(root, "story-btn", Logger);
            quitButton.RegisterCallback<ClickEvent>(PressStoryModeButton);
        }

        private void PressStoryModeButton(ClickEvent _)
        {
            Logger.Debug?.Log("Story mode button clicked");
            Events.Publish(new ChangeSceneEvt(Scene.InGame));
        }
    }
}
