using System.Threading;
using Game.Core.Services;

public partial class GameManager
{
    /// <summary>Instancia serviços básicos para o funcionamento de tudo.</summary>
    private (IEvents, I18n, IGameLogger) SetupServices()
    {
        const LogLvl logLvl =
#if DEBUG
        LogLvl.Debug;
#else
        LogLvl.Info;
#endif
        var logger = new UnityLogger(logLvl);
        var events = new UnityEvents(SynchronizationContext.Current, logger);
        var scenes = new Scenes(events, logger);
        var i18n = new I18nImpl(events, logger);

        Service.Register(events, i18n, logger, privateSvcs: new[] { scenes });
        logger.Info?.Log("Setup services complete");
        return (events, i18n, logger);
    }
}
