namespace Game.Core.Services;

/// <summary>
/// Serviço de log apartado para possivel extensibilidade e telemetria.<br/>
/// <b>ATENÇÃO:</b> as propriedades são nullable para menor alocação de memória em Release
/// (quando Debug estará desativado).
/// <example>
/// Exemplo de uso:
/// <code>
/// logger.Debug?.Log("Hello world");
/// logger.Error?.Log("This is an error", exception);
/// </code>
/// </example>
/// </summary>
/// <remarks>
/// O nome é `IGameLogger` para evitar conflitos de nome com o `ILogger` do unity.
/// </remarks>
public interface IGameLogger
{
    /// <inheritdoc cref="LogLvl.Debug" />
    IMsgLogger? Debug { get; }

    /// <inheritdoc cref="LogLvl.Info" />
    IMsgLogger? Info { get; }

    /// <inheritdoc cref="LogLvl.Warn" />
    IExceptionMsgLogger? Warn { get; }

    /// <inheritdoc cref="LogLvl.Error" />
    IExceptionMsgLogger? Error { get; }
}

/// <inheritdoc cref="IGameLogger"/>
public interface IMsgLogger
{
    /// <inheritdoc cref="IGameLogger"/>
    void Log(string message);
}

/// <inheritdoc cref="IGameLogger"/>
public interface IExceptionMsgLogger : IMsgLogger
{
    /// <inheritdoc cref="IGameLogger"/>
    void Log(string message, Exception ex);
}

public sealed class NullLogger : IGameLogger
{
    public IMsgLogger? Debug => null;

    public IMsgLogger? Info => null;

    public IExceptionMsgLogger? Error => null;

    public IExceptionMsgLogger? Warn => null;

    private NullLogger() { }

    public static IGameLogger Instance { get; } = new NullLogger();
}
