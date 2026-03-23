using Game.Core.Services;
using UnityEngine.InputSystem;

public partial class GameSetup
{
    /// <summary>Instancia serviços básicos para o funcionamento de tudo.</summary>
    private (IScenes, IGameLogger) SetupServices()
    {
        var logLvl =
#if DEBUG
        LogLvl.Debug;
#else
        LogLvl.Info;
#endif
        var logger = new UnityLogger(logLvl);
        var events = new Events(logger);
        var scenes = new Scenes(events, logger);

        Service.Setup(events, scenes, logger);
        logger.Info?.Log("Setup services complete");
        return (scenes, logger);
    }
}
