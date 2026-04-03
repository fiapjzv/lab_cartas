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
        private VisualElement _root = null!;
        private VisualElement _screen = null!;

        public void Awake()
        {
            _logger = Service.Get<IGameLogger>();
            _events = Service.Get<IEvents>();

            var uiDocument = Guard.NotNull(GetComponent<UIDocument>(), _logger);
            _root = uiDocument.rootVisualElement;

            _screen = Guard.ElementIsPresent(_root, SCREEN_ROOT, _logger);
            _progressBar = Guard.ElementIsPresent(_root, PROGRESS_FILL_UI_ELEM, _logger);
            _spinner = Guard.ElementIsPresent(_root, SNIPPER_UI_ELEM, _logger);

            _events.Subscribe<SceneLoadStartEvt>(ShowScreen);
            _events.Subscribe<SceneLoadProgressEvt>(IncreaseProgressBar);
            _events.Subscribe<SceneLoadCompleteEvt>(CompleteProgressBar);

            _events.Subscribe<LoadingScreen100Percent>(HideScreen);
            _events.Subscribe<LoadingScreen100Percent>(AllowUnitySceneLoading);
        }

        private const string SCREEN_ROOT = "root";
    }

    public readonly struct LoadingScreen100Percent { }
}
