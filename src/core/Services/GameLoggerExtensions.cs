using Game.Core.Utils;

namespace Game.Core.Services;

public static class GameLoggerExtensions
{
    /// <summary>Loga o resultado de uma operação.</summary>
    public static void LogResult<T>(
        this IGameLogger logger,
        Result<T> result,
        LogLvl successLvl = LogLvl.Info,
        LogLvl errorLvl = LogLvl.Error
    ) => DoLogResult(logger, mask: null, result, successLvl, errorLvl);

    /// <summary>Loga o resultado de uma operação usando uma máscara de string.Format() .</summary>
    public static void LogResult<T>(
        this IGameLogger logger,
        string mask,
        Result<T> result,
        LogLvl successLvl = LogLvl.Info,
        LogLvl errorLvl = LogLvl.Error
    ) => DoLogResult(logger, mask, result, successLvl, errorLvl);

    private static void DoLogResult<T>(
        IGameLogger logger,
        string? mask,
        Result<T> result,
        LogLvl successLvl = LogLvl.Info,
        LogLvl errorLvl = LogLvl.Error
    )
    {
        var logLvl = result.IsOk() ? successLvl : errorLvl;

        IMsgLogger? msgLogger = logLvl switch
        {
            LogLvl.Debug => logger.Debug,
            LogLvl.Info => logger.Info,
            LogLvl.Warn => logger.Warn,
            LogLvl.Error => logger.Error,
            _ => throw new ArgumentOutOfRangeException(logLvl.ToString()),
        };

        if (msgLogger is null)
        {
            return;
        }

        var msg = mask is null ? result.ToString() : string.Format(mask, result);
        msgLogger.Log(msg);
    }
}
