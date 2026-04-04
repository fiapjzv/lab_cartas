using System;
using Game.Core.Services;

/// <summary>Centro focal para registro de serviços.</summary>
/// <remarks>Propriedades são preenchidas em <see cref="GameSetup.Awake"/>.</remarks>
public static class Service
{
    private static IEvents? _events;
    private static IScenes? _scenes;
    private static I18n? _i18n;
    private static IGameLogger? _logger;

    /// <summary>Retorna um serviço to tipo <paramtype cref="T"/></summary>
    /// <remarks>Propriedades são preenchidas em <see cref="GameSetup.Awake"/>.</remarks>
    public static T Get<T>()
        where T : class
    {
        return (T)ResolveService<T>();
    }

    internal static void Setup(IEvents events, IScenes scenes, I18n i18n, IGameLogger logger)
    {
        _events = events;
        _scenes = scenes;
        _i18n = i18n;
        _logger = logger;
    }

    private static object ResolveService<T>()
    {
        object? svc =
            typeof(T) == typeof(IEvents) ? _events
            : typeof(T) == typeof(IScenes) ? _scenes
            : typeof(T) == typeof(I18n) ? _i18n
            : typeof(T) == typeof(IGameLogger) ? _logger
            : null;

        return (T)(svc ?? throw new Exception($"Service {typeof(T)} not registered yet!"));
    }
}
