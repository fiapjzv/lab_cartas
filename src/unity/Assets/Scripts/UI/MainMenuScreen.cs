using UnityEngine.UIElements;

namespace Game.UI
{
    /// <summary>Comportamento da tela de menu principal do jogo.</summary>
    public partial class MainMenuScreen : GameBehavior
    {
        protected override void Init()
        {
            var uiDocument = Guard.NotNull(GetComponent<UIDocument>(), Logger);
            var root = uiDocument.rootVisualElement;

            BindQuitButton(root);
            BindStoryModeButton(root);
        }
    }
}
