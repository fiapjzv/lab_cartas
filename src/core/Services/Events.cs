using System.Threading;

namespace Game.Core.Services;

/// <summary>
/// Central de eventos usada para desacoplar sistemas através do padrão
/// <example>
/// <code>
/// var events = Events.Instance;
///
/// var sub = events.Subscribe&lt;CardPlayed&gt;(evt =&gt;
/// {
///     Debug.Log($&quot;Carta jogada: {evt.Card}&quot;);
/// });
///
/// events.Publish(new CardPlayed(card));
///
/// sub.Dispose(); // opcionalmente
/// </code>
/// </example>
/// </summary>
/// <remarks>
/// Prefira eventos que sejam <pre>readonly struct</pre> para evita alocações na head durante a publicação.
/// </remarks>
public interface IEvents
{
    /// <summary>Publica uma evento para todos os inscritos no tipo do evento.</summary>
    /// <inheritdoc cref="IEvents"/>
    void Publish<TEvt>(in TEvt evt);

    /// <summary>
    /// Assinatura de um evento.<br />
    /// Registra um <paramref name="handler"/>, ou seja, uma função para ser executada quando um evento acontece.
    /// Retorna um <see cref="IDisposable"/> que remove a assinatura quando chamado.
    /// </summary>
    /// <inheritdoc cref="IEvents"/>
    IDisposable Subscribe<TEvt>(Action<TEvt> handler);

    /// <inheritdoc cref="IEvents.Subscribe{TEvt}(Action{TEvt})"/>
    IDisposable Subscribe<TEvt>(Func<TEvt, Task> handler);
}

/// <inheritdoc cref="IEvents"/>
public abstract partial class Events : IEvents
{
    private readonly IGameLogger _logger;

    // NOTE: sendo thread-safe lockando o dicionário de handlers apenas em Subscribe
    private readonly object _subLock = new();
    private Dictionary<Type, object> _syncHandlers = new();
    private Dictionary<Type, object> _asyncHandlers = new();

    protected Events(IGameLogger? logger = null)
    {
        _logger = logger ?? NullLogger.Instance;
    }
}
