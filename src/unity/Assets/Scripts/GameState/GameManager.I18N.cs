using Game.Core.Services;

public partial class GameManager
{
    private void StartI18N(I18N i18n, IGameLogger logger)
    {
        i18n.Start();
        logger.Info?.Log("Local I18N labels loaded. Starting to sync with server.");
    }
}
