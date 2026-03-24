using System.Diagnostics;

namespace Game.Core.Services;

public partial class Events
{
    /// <inheritdoc cref="IEvents.Publish{TEvt}(in TEvt)" />
    public void Publish<TEvt>(in TEvt evt)
    {
        var evtType = typeof(TEvt);
        if (!_handlers.TryGetValue(evtType, out var listObj))
        {
            _logger.Warn?.Log($"No listenters for event: {evt}");
            return;
        }

        var handlersList = (listObj as List<Action<TEvt>>)!;
        _logger.Debug?.Log(
            $"Event published: {evtType.Name}! Subscribers count: {handlersList.Count}"
        );
        for (var i = 0; i < handlersList.Count; i++)
        {
            var handler = handlersList[i];
            try
            {
                // NOTE: assume que vamos rodar em Release sem o LogLevel Debug
                if (_logger.Debug is null)
                {
                    handler(evt);
                }
                else
                {
                    ExecHandlerLogging(handler, evt);
                }
            }
            catch (Exception e)
            {
                _logger.Error?.Log($"Unexpected error running {handler} for {evt}", e);
            }
        }
    }

    private void ExecHandlerLogging<TEvt>(Action<TEvt> handler, TEvt evt)
    {
        _logger.Debug?.Log($"Starting to run handler {handler.TryGetName()} for {evt}");
        var sw = Stopwatch.StartNew();
        handler(evt);
        _logger.Debug?.Log(
            $"Handler {handler.TryGetName()} ran for {evt} in {sw.ElapsedMilliseconds:F} ms"
        );
    }
}
