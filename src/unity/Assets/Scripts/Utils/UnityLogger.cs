using System;
using Game.Core.Services;
using UnityDebug = UnityEngine.Debug;

public class UnityLogger : IGameLogger
{
    public IMsgLogger? Debug { get; }
    public IMsgLogger? Info { get; }
    public IExceptionMsgLogger? Warn { get; }
    public IExceptionMsgLogger? Error { get; }

    public UnityLogger(LogLvl lvl)
    {
        Debug = lvl <= LogLvl.Debug ? new MsgLogger(UnityDebug.Log, LogLvl.Debug) : null;
        Info = lvl <= LogLvl.Info ? new MsgLogger(UnityDebug.Log, LogLvl.Info) : null;
        Warn = lvl <= LogLvl.Warn ? new ExMsgLogger(UnityDebug.LogWarning, LogLvl.Warn) : null;
        Error = lvl <= LogLvl.Error ? new ExMsgLogger(UnityDebug.LogError, LogLvl.Error) : null;

        // _type = GetType().FullName ?? GetType().Name;
    }

    private class MsgLogger : IMsgLogger
    {
        private readonly Action<string> _log;
        private readonly string _levelPrefix;

        public MsgLogger(Action<string> log, LogLvl level)
        {
            _log = log;
            _levelPrefix = level.ToString();
        }

        public void Log(string message)
        {
            _log($"[{_levelPrefix}] {message}");
        }
    }

    private class ExMsgLogger : MsgLogger, IExceptionMsgLogger
    {
        public ExMsgLogger(Action<string> log, LogLvl level)
            : base(log, level) { }

        public void Log(string message, Exception ex)
        {
            Log(message);
            UnityDebug.LogException(ex);
        }
    }
}
