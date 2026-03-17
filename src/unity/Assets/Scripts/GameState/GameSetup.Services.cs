using Game.Core.Services;

public partial class GameSetup
{
    /// <summary>Instancia serviços básicos para o funcionamento de tudo.</summary>
    private IGameLogger SetupServices()
    {
        var logLvl =
#if DEBUG
        LogLvl.Debug;
#else
        LogLvl.Info;
#endif
        var logger = new UnityLogger(logLvl);
        var events = new Events(logger);

        Services.Setup(events, logger);
        logger.Info?.Log("Setup services complete");
        return logger;
    }
}
