namespace Game.Core.Services;

public partial class Events
{
    /// <inheritdoc cref="IEvents.Publish{TEvt}(in TEvt)" />
    public void Publish<TEvt>(in TEvt evt)
    {
        if (!_handlers.TryGetValue(typeof(TEvt), out var listObj))
        {
            return;
        }

        var handlersList = listObj as List<Action<TEvt>>;
        foreach (var handler in handlersList!)
        {
            try
            {
                handler(evt);
            }
            catch (Exception e)
            {
                // TODO: log error
                // logger.Error();
            }
        }
    }
}
