using Game.Core.Services;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UI
{
    public partial class LoadingScreen : MonoBehaviour
    {
        private IGameLogger _logger = null!;

        private void Awake()
        {
            _logger = Service.Get<IGameLogger>();

            var uiDocument = Guard.NotNull(GetComponent<UIDocument>(), _logger);
            var root = uiDocument.rootVisualElement;

            EnsureSpinnerElement(root);
        }

        private void OnDisable()
        {
            HideSpinner();
        }

        private void OnEnable()
        {
            ShowSpinner();
        }
    }
}
