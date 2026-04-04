using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Core.Services;
using Game.Core.Utils;

/// <summary>Serviço de internacionalização de textos.</summary>
public interface I18n
{
    /// <summary>Carrega síncronamente o que consegue de labels.</summary>
    void Start(string[] mandatorySections);

    /// <summary>
    /// Contacta o servidor para buscar um grupo de chaves e traduções <see cref="I18nSection" />
    /// </summary>
    Task<Result<I18nSection>> ForSection(string sectionKey);

    /// <summary>
    /// Limpa cache local de traduções.<br/>
    /// Usado principalmente quando o jogador trocar de língua.
    /// </summary>
    void ResetCache();
}

/// <summary>
/// Contacta o servidor ou um cache local de arquivos para carregar traduções na língua do jogador.
/// </summary>
public partial class I18nImpl : I18n
{
    private readonly IGameLogger _logger;
    private readonly Dictionary<string, I18nSection> _loadedSections = new();

    public I18nImpl(IEvents events, IGameLogger? logger = null)
    {
        _logger = logger ?? NullLogger.Instance;
    }

    /// <inheritdoc/>
    public void ResetCache()
    {
        // TODO: resetar cache local
        var error = $"Implement {nameof(I18nImpl)}.{nameof(ResetCache)}!";
        _logger.Error?.Log(error);
        throw new NotImplementedException(error);
    }
}
