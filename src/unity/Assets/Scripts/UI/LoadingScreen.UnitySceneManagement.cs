namespace Game.UI
{
    public partial class LoadingScreen
    {
        private SceneLoadCompleteEvt? _sceneLoadComplete;

        private void AllowUnitySceneLoading(LoadingScreen100Percent _)
        {
            var sceneLoadComplete = _sceneLoadComplete;
            if (sceneLoadComplete is null)
            {
                _logger.Error?.Log(
                    $"No {nameof(SceneLoadCompleteEvt)} on {nameof(LoadingScreen)}!"
                );
                return;
            }

            // NOTE: this tells unity to display the next scene
            sceneLoadComplete.SceneLoaded.allowSceneActivation = true;
        }
    }
}
