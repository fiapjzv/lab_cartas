using Game.Core.Services;

/// <summary>Centro focal para registro de serviços.</summary>
/// <remarks>Propriedades são preenchidas em <see cref="GameSetup.Awake"/>.</remarks>
public class Services
{
    /// <inheritdoc cref="IGameLogger" />
    private static IGameLogger? _logger;

    /// <inheritdoc cref="IEvents" />
    private static IEvents? _events;

    /// <summary>Retorna um serviço to tipo <paramtype cref="T"/></summary>
    /// <remarks>Propriedades são preenchidas em <see cref="GameSetup.Awake"/>.</remarks>
    public static T Get<T>()
        where T : class
    {
        return (T)ResolveService<T>();
    }

    internal static void Setup(IEvents events, IGameLogger logger)
    {
        _events = events;
        _logger = logger;
    }

    private static object ResolveService<T>()
    {
        if (typeof(T) == typeof(IGameLogger))
        {
            return _logger ?? throw new Exception($"Service {typeof(T)} not registered yet!");
        }

        if (typeof(T) == typeof(IEvents))
        {
            return _events ?? throw new Exception($"Service {typeof(T)} not registered yet!");
        }

        throw new Exception($"Unknown service: {typeof(T)}");
    }
}
