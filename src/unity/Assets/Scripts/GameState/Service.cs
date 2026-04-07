using System;
using System.Linq;
using Game.Core.Services;

/// <summary>Centro focal para registro de serviços.</summary>
/// <remarks>Propriedades são preenchidas em <see cref="GameSetup.Awake"/>.</remarks>
public static class Service
{
    private static IEvents? _events;
    private static I18n? _i18n;
    private static IGameLogger? _logger;

    // NOTE: only holding references for private services to avoid them to be garbage collected
    private static object[]? _privateSvcs;

    /// <summary>Retorna um serviço to tipo <paramtype cref="T"/></summary>
    /// <remarks>Propriedades são preenchidas em <see cref="GameSetup.Awake"/>.</remarks>
    public static T Get<T>()
        where T : class
    {
        return (T)ResolveService<T>();
    }

    internal static void Register(
        IEvents events,
        I18n i18n,
        IGameLogger logger,
        object[] privateSvcs
    )
    {
        _events = events;
        _i18n = i18n;
        _logger = logger;
        _privateSvcs = privateSvcs;
    }

    private static object ResolveService<T>()
    {
        var type = typeof(T);
        object? svc =
            type == typeof(IEvents) ? _events
            : type == typeof(I18n) ? _i18n
            : type == typeof(IGameLogger) ? _logger
            : null;

        if (svc is null && _privateSvcs.Any(pvt => pvt.GetType() == type))
        {
            throw new Exception(
                "You are not supposed to solve private services. Use events to trigger them."
            );
        }

        return (T)(svc ?? throw new Exception($"Service {typeof(T)} not registered yet!"));
    }
}
