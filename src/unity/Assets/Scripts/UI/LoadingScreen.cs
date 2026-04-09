using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Game.UI
{
    /// <summary>Comportamento da tela de loading.</summary>
    public partial class LoadingScreen : GameBehavior
    {
        private VisualElement _root = null!;
        private VisualElement _screen = null!;

        protected override void Init()
        {
            var uiDocument = Guard.NotNull(GetComponent<UIDocument>(), Logger);
            _root = uiDocument.rootVisualElement;

            _screen = Guard.ElementIsPresent(_root, SCREEN_ROOT, Logger);
            _progressBar = Guard.ElementIsPresent(_root, PROGRESS_FILL_UI_ELEM, Logger);
            _spinner = Guard.ElementIsPresent(_root, SNIPPER_UI_ELEM, Logger);
        }

        protected override IEnumerable<IDisposable> SubscribeEvents()
        {
            yield return Events.Subscribe<SceneLoadStartEvt>(ShowScreen);
            yield return Events.Subscribe<SceneLoadProgressEvt>(IncreaseProgressBar);
            yield return Events.Subscribe<SceneLoadCompleteEvt>(CompleteProgressBar);

            yield return Events.Subscribe<LoadingScreen100Percent>(HideScreen);
            yield return Events.Subscribe<LoadingScreen100Percent>(AllowUnitySceneLoading);
        }

        private const string SCREEN_ROOT = "root";
    }

    public readonly struct LoadingScreen100Percent { }
}
