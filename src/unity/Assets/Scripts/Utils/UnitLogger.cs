using Game.Core.Services;
using UnityDebug = UnityEngine.Debug;

public class UnityLogger : Game.Core.Services.ILogger
{
    public IMsgLogger? Debug { get; }
    public IMsgLogger? Info { get; }
    public IExceptionMsgLogger? Warn { get; }
    public IExceptionMsgLogger? Error { get; }

    public UnityLogger(Game.Core.Services.LogLevel lvl)
    {
        Debug = lvl <= LogLevel.Debug ? new MsgLogger(UnityDebug.Log, LogLevel.Debug) : null;
        Info = lvl <= LogLevel.Info ? new MsgLogger(UnityDebug.Log, LogLevel.Info) : null;
        Warn = lvl <= LogLevel.Warn ? new ExMsgLogger(UnityDebug.LogWarning, LogLevel.Warn) : null;
        Error = lvl <= LogLevel.Error ? new ExMsgLogger(UnityDebug.LogError, LogLevel.Error) : null;
    }

    private class MsgLogger : IMsgLogger
    {
        private readonly Action<string> log;
        private readonly string levelPrefix;

        public MsgLogger(Action<string> log, LogLevel level)
        {
            this.log = log;
            this.levelPrefix = level.ToString();
        }

        public void Log(string message)
        {
            log($"[{levelPrefix}] message");
        }
    }

    private class ExMsgLogger : MsgLogger, IExceptionMsgLogger
    {
        public ExMsgLogger(Action<string> log, LogLevel level)
            : base(log, level) { }

        public void Log(string message, Exception ex)
        {
            base.Log(message);
            UnityDebug.LogException(ex);
        }
    }
}
