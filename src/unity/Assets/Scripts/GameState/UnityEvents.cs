using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Game.Core.Services;

/// <inheritdoc cref="IEvents"/>
/// <remarks>Em um projeto unity precisamos garantir que os eventos aconteçam na thread principal.</remarks>
public class UnityEvents : Events
{
    private readonly SynchronizationContext _syncContext;

    /// <inheritdoc cref="UnityEvents" />
    public UnityEvents(SynchronizationContext syncContext, IGameLogger? logger = null)
        : base(logger)
    {
        _syncContext = syncContext;
    }

    protected override void ScheduleSyncHandlersRun<TEvt>(List<Action<TEvt>> handlers, TEvt evt) =>
        _syncContext.Post(_ => SyncHandlersRun(handlers, evt), null);

    protected override void ScheduleAsyncHandlersRun<TEvt>(List<Func<TEvt, Task>> handlers, TEvt evt) =>
        _syncContext.Post(_ => _ = AsyncHandlersRun(handlers, evt), null);
}
