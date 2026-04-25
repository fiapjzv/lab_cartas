using System.Threading;
using Game.Core.Services;

public partial class GameManager
{
    /// <summary>Instancia serviços básicos para o funcionamento de tudo.</summary>
    private static (IEvents, I18n, IGameLogger) SetupBasicServices()
    {
        const LogLvl logLvl =
#if DEBUG
        LogLvl.Debug;
#else
        LogLvl.Info;
#endif
        var logger = new UnityLogger(logLvl);
        var events = new UnityEvents(SynchronizationContext.Current, logger);
        var i18n = new I18nImpl(events, logger);
        var scenes = new Scenes(events, logger);

        Service.Register<IGameLogger>(logger);
        Service.Register<IEvents>(events);
        Service.Register<I18n>(i18n);
        Service.RegisterPrivate(scenes);
        logger.Info?.Log("Setup services complete");
        return (events, i18n, logger);
    }

    private static void SetupGameServices()
    {
        // TODO: gather real info from server
        var playerAccount = new PlayerAccount();
        var playerDecks = new PlayerDecks();
        var matchMaking = new BattleMatchMaker();
        Service.Register<IPlayerAccount>(playerAccount);
        Service.Register<IPlayerDecks>(playerDecks);
        Service.Register<IBattleMatchMaker>(matchMaking);
    }
}
