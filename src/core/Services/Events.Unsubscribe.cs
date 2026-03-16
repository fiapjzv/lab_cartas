namespace Game.Core.Services;

public partial class Events
{
    internal void Unsubscribe<TEvt>(Action<TEvt> handler)
    {
        var type = typeof(TEvt);
        lock (_subLock)
        {
            if (!_handlers.TryGetValue(type, out var currHandlers))
            {
                // TODO: log
                // _logger.
                return;
            }

            var newHandlers = new List<Action<TEvt>>((List<Action<TEvt>>)currHandlers);
            newHandlers.Remove(handler);
            _handlers[type] = newHandlers;
        }
    }
}
