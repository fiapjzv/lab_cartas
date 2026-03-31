using Game.Core.Services;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UI
{
    /// <summary>Comportamento da tela de loading.</summary>
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

        /// <summary>Tempo mínimo na página de loading (para garantir animações).</summary>
        private const float MIN_PROGRESS_DURATION_MS = 0.6f;
    }
}
