using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Core.Services;
using Game.Core.Utils;

/// <summary>Serviço de internacionalização de textos.</summary>
public interface I18n
{
    /// <summary>Língua atuala do jogo.</summary>
    string Locale { get; }

    /// <summary>Carrega síncronamente o que consegue de labels.</summary>
    void Start(string[] mandatorySections);

    /// <summary>
    /// Contacta o servidor para buscar um grupo de chaves e traduções <see cref="I18nSection" />
    /// </summary>
    Task<Result<I18nSection>> ForSection(string sectionKey);
}

/// <summary>
/// Contacta o servidor ou um cache local de arquivos para carregar traduções na língua do jogador.
/// </summary>
public partial class I18nImpl : I18n
{
    /// <inheritdoc/>
    public string Locale { get; private set; }

    private readonly IGameLogger _logger;
    private readonly Dictionary<string, I18nSection> _loadedSections = new();

    public I18nImpl(IEvents events, IGameLogger? logger = null)
    {
        // TODO: get player locale (ex: "en_US", "pt_BR")
        Locale = "pt_BR";
        _logger = logger ?? NullLogger.Instance;
    }

    // TODO: change locale with event
    // /// <inheritdoc/>
    // public void ChangeLocale(string locale)
    // {
    //     // TODO: resetar cache local
    //     var error = $"Implement {nameof(I18nImpl)}.{nameof(ChangeLocale)}!";
    //     _logger.Error?.Log(error);
    //     throw new NotImplementedException(error);
    // }
}
