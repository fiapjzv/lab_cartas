namespace Game.Core.Services;

public partial class Events
{
    /// <inheritdoc cref="IEvents.Subscribe{TEvt}(Action{TEvt})" />
    public IDisposable Subscribe<TEvt>(Action<TEvt> action)
    {
        // PERF: provavelmente seria melhor usar arrays invés de listas
        // PERF: pré-alocação de todos os eventos pra não precisar recriar
        var type = typeof(TEvt);
        lock (_subLock)
        {
            var handlersList = _handlers.TryGetValue(type, out var currHandlers)
                ? new((List<Action<TEvt>>)currHandlers) { action }
                : new List<Action<TEvt>> { action };

            _handlers = new Dictionary<Type, object>(_handlers) { [type] = handlersList };
            _logger.Debug?.Log(
                $"Event {type.Name} subscribed with {action.TryGetName()}! Total subscriptions {handlersList.Count}"
            );
        }

        return new Subscription<TEvt>(this, action);
    }

    private class Subscription<TEvt> : IDisposable
    {
        private readonly Events _events;
        private readonly Action<TEvt> _action;

        private bool disposed;

        public Subscription(Events events, Action<TEvt> action)
        {
            _events = events;
            _action = action;
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            _events.Unsubscribe(_action);
        }
    }
}
