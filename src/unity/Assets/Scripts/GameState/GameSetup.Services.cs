using Game.Core.Services;

public partial class GameSetup
{
    /// <summary>Instancia serviços básicos para o funcionamento de tudo.</summary>
    private void SetupServices()
    {
        var logLvl =
#if DEBUG
        Game.Core.Services.LogLevel.Debug;
#else
        Game.Core.Services.LogLevel.Info;
#endif
        logger = new UnityLogger(logLvl);
        events = new Events(logger);

        logger.Info?.Log("Setup services complete");
    }
}
