/// <summary>Centro focal para registro de serviços.</summary>
/// <remarks>Propriedades são preenchidas em <see cref="GameSetup.Awake"/>.</remarks>
public static class Service
{
    /// <inheritdoc cref="IEvents" />
    private static IEvents? _events;

    /// <inheritdoc cref="IScenes" />
    private static IScenes? _scenes;

    /// <inheritdoc cref="IGameLogger" />
    private static IGameLogger? _logger;

    /// <summary>Retorna um serviço to tipo <paramtype cref="T"/></summary>
    /// <remarks>Propriedades são preenchidas em <see cref="GameSetup.Awake"/>.</remarks>
    public static T Get<T>()
        where T : class
    {
        return (T)ResolveService<T>();
    }

    internal static void Setup(IEvents events, IScenes scenes, IGameLogger logger)
    {
        _events = events;
        _scenes = scenes;
        _logger = logger;
    }

    private static object ResolveService<T>()
    {
        object? svc =
            typeof(T) == typeof(IEvents) ? _events
            : typeof(T) == typeof(IScenes) ? _scenes
            : typeof(T) == typeof(IGameLogger) ? _logger
            : null;

        return (T)(svc ?? throw new Exception($"Service {typeof(T)} not registered yet!"));
    }
}
