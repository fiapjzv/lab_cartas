using UnityEngine.UIElements;

namespace Game.UI
{
    public partial class MainMenuScreen
    {
        private void BindQuitButton(VisualElement root)
        {
            var quitButton = Guard.ElementIsPresent<Button>(root, "quit-btn", _logger);
            quitButton.RegisterCallback<ClickEvent>(PressQuitButton);
        }

        private void PressQuitButton(ClickEvent clickEvt)
        {
            var quitReason = "Main menu quit button clicked";
            _logger.Debug?.Log($"{quitReason} - {clickEvt}");
            // TODO: modal de tem certeza que deseja sair
            _events.Publish(new QuitEvent(quitReason));
        }
    }
}
