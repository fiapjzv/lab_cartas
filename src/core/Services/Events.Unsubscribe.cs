namespace Game.Core.Services;

public partial class Events
{
    internal void UnsubscribeSyncHandler<TEvt>(Action<TEvt> handler)
    {
        var evtType = typeof(TEvt);
        lock (_subLock)
        {
            if (!_syncHandlers.TryGetValue(evtType, out var syncHandlers))
            {
                _logger.Warn?.Log(
                    $"Could not find sync subscriber {handler.TryGetName()} for event {evtType}"
                );
                return;
            }

            var newHandlers = new List<Action<TEvt>>((List<Action<TEvt>>)syncHandlers);
            newHandlers.Remove(handler);
            _syncHandlers[evtType] = newHandlers;

            _logger.Debug?.Log(
                $"{handler.TryGetName()} unsubscribed event {evtType.Name}! Total subscriptions {newHandlers.Count}"
            );
        }
    }

    internal void UnsubscribeAsyncHandler<TEvt>(Func<TEvt, Task> handler)
    {
        var evtType = typeof(TEvt);
        lock (_subLock)
        {
            if (!_asyncHandlers.TryGetValue(evtType, out var asyncHandlers))
            {
                _logger.Warn?.Log(
                    $"Could not find async subscriber {handler.TryGetName()} for event {evtType}"
                );
                return;
            }

            var newHandlers = new List<Func<TEvt, Task>>((List<Func<TEvt, Task>>)asyncHandlers);
            newHandlers.Remove(handler);
            _asyncHandlers[evtType] = newHandlers;

            _logger.Debug?.Log(
                $"{handler.TryGetName()} unsubscribed event {evtType.Name}! Total subscriptions {newHandlers.Count}"
            );
        }
    }
}
