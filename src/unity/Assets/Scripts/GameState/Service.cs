using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Services;

/// <summary>Central hub for service registration and resolution.</summary>
/// <remarks>Properties are typically populated in <see cref="GameManager.Awake"/>.</remarks>
public static class Service
{
    private static readonly Dictionary<Type, object> _services = new();

    // NOTE: References to private services stored specifically to prevent Garbage Collection
    private static object[] _privateSvcs = Array.Empty<object>();

    /// <summary>Retrieves a service of type <typeparamref name="T"/></summary>
    /// <remarks>
    /// This method is NOT thread-safe. Ensure <see cref="Register"/> is only called during
    /// the initial startup (e.g., <see cref="GameManager.Awake"/>).
    /// </remarks>
    public static T Get<T>()
        where T : class
    {
        return (T)ResolveService<T>();
    }

    /// <summary>Registers public services available for global access.</summary>
    /// <remarks>
    /// This method is NOT thread-safe. Ensure <see cref="Register"/> is only called during
    /// the initial startup (e.g., <see cref="GameManager.Awake"/>).
    /// </remarks>
    public static void Register<TI>(TI svc)
    {
        _services[typeof(TI)] = svc!;
    }

    /// <summary>Registers private services that should exist in memory but cannot be resolved.</summary>
    /// <remarks>
    /// This enforces an event-driven architecture for internal systems (AKA: use <see cref="IEvents"/>).
    /// </remarks>
    public static void RegisterPrivate(params object[] privateSvcs)
    {
        _privateSvcs = _privateSvcs.Concat(privateSvcs).ToArray();
    }

    private static object ResolveService<T>()
    {
        var type = typeof(T);
        if (_services.TryGetValue(type, out var svc))
        {
            return svc;
        }

        if (_privateSvcs.Any(pvt => pvt.GetType() == type))
        {
            throw Guard.Panic($"Access Denied: {type.Name} is a private service. Use IEvents to interact with it.");
        }

        throw Guard.Panic($"Trying to resolve service {type.Name} that was not registered yet!");
    }
}
