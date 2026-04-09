using Game.Core.Services;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UI
{
    /// <summary>Comportamento da tela de menu principal do jogo.</summary>
    public partial class MainMenuScreen : MonoBehaviour
    {
        private IGameLogger _logger = null!;
        private IEvents _events = null!;

        private void Awake()
        {
            _logger = Service.Get<IGameLogger>();
            _events = Service.Get<IEvents>();

            var uiDocument = Guard.NotNull(GetComponent<UIDocument>(), _logger);
            var root = uiDocument.rootVisualElement;

            BindQuitButton(root);
            BindStoryModeButton(root);
        }
    }
}
