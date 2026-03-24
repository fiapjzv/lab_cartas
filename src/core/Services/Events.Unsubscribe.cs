namespace Game.Core.Services;

public partial class Events
{
    internal void Unsubscribe<TEvt>(Action<TEvt> handler)
    {
        var evtType = typeof(TEvt);
        lock (_subLock)
        {
            if (!_handlers.TryGetValue(evtType, out var currHandlers))
            {
                _logger.Warn?.Log(
                    $"Could not find subscriber {handler.TryGetName()} for event {evtType}"
                );
                return;
            }

            var newHandlers = new List<Action<TEvt>>((List<Action<TEvt>>)currHandlers);
            newHandlers.Remove(handler);
            _handlers[evtType] = newHandlers;

            _logger.Debug?.Log(
                $"{handler.TryGetName()} unsubscribed event {evtType.Name}! Total subscriptions {newHandlers.Count}"
            );
        }
    }
}
