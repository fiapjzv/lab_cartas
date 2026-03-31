using System.Threading.Tasks;
using Game.Core.Services;

/// <summary>Serviço de internacionalização de textos.</summary>
public interface I18N
{
    /// <summary></summary>
    I18NSection ForSection(string sectionKey);

    /// <summary>
    /// Limpa cache local de traduções.<br/>
    /// Usado principalmente quando o jogador trocar de língua.
    /// </summary>
    void ResetCache();
}

/// <inheritdoc/>
public class I18NClient : I18N
{
    private IGameLogger _logger;

    public I18NClient(IGameLogger? logger = null)
    {
        _logger = logger ?? NullLogger.Instance;
    }

    /// <inheritdoc/>
    public I18NSection ForSection(string sectionKey)
    {
        return new I18NSectionFetcher(this, sectionKey, _logger);
    }

    /// <inheritdoc/>
    public void ResetCache()
    {
        // TODO: resetar cache local
    }
}

/// <summary>Representa um agrupamento de chaves (ex: "menu").</summary>
public interface I18NSection
{
    /// <summary>Identificador da seção. Exemplo: "menu", "enemy-cards", "global"</summary>
    string Key { get; }

    /// <summary>Tradução do texto na língua do jogador.</summary>
    Task<string> Label(string i18nKey);
}

/// <inheritdoc/>
public class I18NSectionFetcher : I18NSection
{
    private I18NClient _i18n;
    private IGameLogger _logger;

    /// <inheritdoc/>
    public string Key { get; }

    /// <inheritdoc cref="II18NSection" />
    public I18NSectionFetcher(I18NClient i18n, string key, IGameLogger? logger = null)
    {
        Key = key;
        _i18n = i18n;
        _logger = logger ?? NullLogger.Instance;
    }

    public Task<string> Label(string i18nKey)
    {
        return Task.FromResult(i18nKey);
    }
}
