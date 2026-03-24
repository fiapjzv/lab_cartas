using Game.Core.Services;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UI
{
    public partial class LoadingScreen : MonoBehaviour
    {
        private IGameLogger _logger = null!;
        private IEvents _events = null!;

        private void Awake()
        {
            _logger = Service.Get<IGameLogger>();
            _events = Service.Get<IEvents>();

            var uiDocument = Guard.NotNull(GetComponent<UIDocument>(), _logger);
            var root = uiDocument.rootVisualElement;

            EnsureSpinnerElement(root);
            EnsureLoadingBar(root);
        }

        private void OnEnable()
        {
            ShowSpinner();
            ResetProgressBar();
        }

        private void OnDisable()
        {
            HideSpinner();
        }
    }
}
