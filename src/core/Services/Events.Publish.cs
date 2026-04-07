using System.Diagnostics;

namespace Game.Core.Services;

public partial class Events
{
    /// <inheritdoc cref="IEvents.Publish{TEvt}(in TEvt)" />
    public void Publish<TEvt>(in TEvt evt)
    {
        var evtType = typeof(TEvt);

        var anyHandlers = false;
        if (_syncHandlers.TryGetValue(evtType, out var syncHandlersObj))
        {
            var syncHandlers = (syncHandlersObj as List<Action<TEvt>>)!;
            _logger.Debug?.Log(
                $"Event published: {evtType.Name} Subscribers: {syncHandlers.Count}"
            );
            ScheduleSyncHandlersRun(syncHandlers, evt);
            anyHandlers = true;
        }

        if (_asyncHandlers.TryGetValue(evtType, out var asyncHandlersObj))
        {
            var asyncHandlers = (asyncHandlersObj as List<Func<TEvt, Task>>)!;
            _logger.Debug?.Log(
                $"Event published: {evtType.Name} Subscribers: {asyncHandlers.Count}"
            );
            ScheduleAsyncHandlersRun(asyncHandlers, evt);
            anyHandlers = true;
        }

        if (!anyHandlers)
        {
            _logger.Warn?.Log($"No subscribers for event: {evt}");
        }
    }

    private void ScheduleSyncHandlersRun<TEvt>(List<Action<TEvt>> handlers, TEvt evt) =>
        _syncContext.Post(
            _ =>
            {
                for (var i = 0; i < handlers.Count; i++)
                {
                    var handler = handlers[i];
                    try
                    {
                        // NOTE: assume que vamos rodar em Release sem o LogLevel Debug
                        if (_logger.Debug is null)
                        {
                            handler(evt);
                        }
                        else
                        {
                            ExecSyncHandlerLogging(handler, evt);
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Error?.Log($"Unexpected error running {handler} for {evt}", e);
                    }
                }
            },
            null
        );

    private void ScheduleAsyncHandlersRun<TEvt>(List<Func<TEvt, Task>> handlers, TEvt evt)
    {
        _syncContext.Post(
            async _ =>
            {
                for (var i = 0; i < handlers.Count; i++)
                {
                    var handler = handlers[i];
                    try
                    {
                        // NOTE: assume que vamos rodar em Release sem o LogLevel Debug
                        if (_logger.Debug is null)
                        {
                            await handler(evt);
                        }
                        else
                        {
                            await ExecAsyncHandlerLogging(handler, evt);
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Error?.Log($"Unexpected error running {handler} for {evt}", e);
                    }
                }
            },
            null
        );
    }

    private void ExecSyncHandlerLogging<TEvt>(Action<TEvt> handler, TEvt evt)
    {
        _logger.Debug?.Log($"Starting to run handler {handler.TryGetName()} for {evt}");
        var sw = Stopwatch.StartNew();
        handler(evt);
        _logger.Debug?.Log(
            $"Handler {handler.TryGetName()} ran for {evt} in {sw.ElapsedMilliseconds:F} ms"
        );
    }

    private async Task ExecAsyncHandlerLogging<TEvt>(Func<TEvt, Task> handler, TEvt evt)
    {
        _logger.Debug?.Log($"Starting to run handler {handler.TryGetName()} for {evt}");
        var sw = Stopwatch.StartNew();
        await handler(evt);
        _logger.Debug?.Log(
            $"Handler {handler.TryGetName()} ran for {evt} in {sw.ElapsedMilliseconds:F} ms"
        );
    }
}
