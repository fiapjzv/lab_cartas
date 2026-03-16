namespace Game.Core.Services;

public interface ILogger { }

public class NullLogger : ILogger
{
    private NullLogger() { }

    public static ILogger Instance { get; } = new NullLogger();
}
