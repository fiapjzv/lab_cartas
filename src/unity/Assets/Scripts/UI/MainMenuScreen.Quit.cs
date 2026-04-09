using UnityEngine.UIElements;

namespace Game.UI
{
    public partial class MainMenuScreen
    {
        private void BindQuitButton(VisualElement root)
        {
            var quitButton = Guard.ElementIsPresent<Button>(root, "quit-btn", Logger);
            quitButton.RegisterCallback<ClickEvent>(PressQuitButton);
        }

        private void PressQuitButton(ClickEvent clickEvt)
        {
            var quitReason = "Main menu quit button clicked";
            Logger.Debug?.Log($"{quitReason} - {clickEvt}");
            // TODO: modal de tem certeza que deseja sair
            Events.Publish(new QuitEvent(quitReason));
        }
    }
}
