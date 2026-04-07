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
            var handlersList = _syncHandlers.TryGetValue(type, out var currHandlers)
                ? new((List<Action<TEvt>>)currHandlers) { action }
                : new List<Action<TEvt>> { action };

            _syncHandlers = new Dictionary<Type, object>(_syncHandlers) { [type] = handlersList };
            _logger.Debug?.Log(
                $"Event {type.Name} subscribed with {action.TryGetName()}! "
                    + $"Total sync subscriptions {handlersList.Count}"
            );
        }

        return Subscription<TEvt>.SyncSub(this, action);
    }

    /// <inheritdoc cref="IEvents.Subscribe{TEvt}(Action{TEvt})" />
    public IDisposable Subscribe<TEvt>(Func<TEvt, Task> action)
    {
        // PERF: provavelmente seria melhor usar arrays invés de listas
        // PERF: pré-alocação de todos os eventos pra não precisar recriar
        var type = typeof(TEvt);
        lock (_subLock)
        {
            var handlersList = _asyncHandlers.TryGetValue(type, out var currHandlers)
                ? new((List<Func<TEvt, Task>>)currHandlers) { action }
                : new List<Func<TEvt, Task>> { action };

            _asyncHandlers = new Dictionary<Type, object>(_asyncHandlers) { [type] = handlersList };
            _logger.Debug?.Log(
                $"Event {type.Name} subscribed with {action.TryGetName()}! "
                    + $"Total async subscriptions {handlersList.Count}"
            );
        }

        return Subscription<TEvt>.AsyncSub(this, action);
    }

    private class Subscription<TEvt> : IDisposable
    {
        private readonly bool _isSync;
        private readonly Events _events;
        private readonly Action<TEvt>? _syncAction;
        private readonly Func<TEvt, Task>? _asyncAction;

        private bool _disposed;

        private Subscription(Events events, Action<TEvt>? syncAction, Func<TEvt, Task>? asyncAction)
        {
            _isSync = syncAction is not null;
            _events = events;
            _syncAction = syncAction;
            _asyncAction = asyncAction;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            if (_isSync)
            {
                _events.UnsubscribeSyncHandler(_syncAction!);
            }
            else
            {
                _events.UnsubscribeAsyncHandler(_asyncAction!);
            }
        }

        public static Subscription<TEvt> SyncSub(Events events, Action<TEvt> action) =>
            new(events, action, asyncAction: null);

        internal static Subscription<TEvt> AsyncSub(Events events, Func<TEvt, Task> action) =>
            new(events, syncAction: null, asyncAction: action);
    }
}
