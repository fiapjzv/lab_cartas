namespace Game.Core.Services;

public interface ILogger
{
    IMsgLogger? Debug { get; }
    IMsgLogger? Info { get; }
    IExceptionMsgLogger? Error { get; }
    IExceptionMsgLogger? Warn { get; }
}

public enum LogLevel
{
    Debug,
    Info,
    Warn,
    Error,
}

public interface IMsgLogger
{
    void Log(string message);
}

public interface IExceptionMsgLogger : IMsgLogger
{
    void Log(string message, Exception ex);
}

public class NullLogger : ILogger
{
    public IMsgLogger? Debug => null;

    public IMsgLogger? Info => null;

    public IExceptionMsgLogger? Error => null;

    public IExceptionMsgLogger? Warn => null;

    private NullLogger() { }

    public static ILogger Instance { get; } = new NullLogger();
}
