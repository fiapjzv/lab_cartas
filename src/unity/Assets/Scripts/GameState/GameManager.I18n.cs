using Game.Core.Services;

public partial class GameManager
{
    private void StartI18n(I18n i18n, IGameLogger logger)
    {
        i18n.Start(new[] { "ui" });
        logger.Info?.Log($"Local {nameof(I18n)} labels loaded. Starting to sync with server.");
    }
}
