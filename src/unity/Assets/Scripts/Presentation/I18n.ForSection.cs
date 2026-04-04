using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Game.Core.Utils;
using UnityEngine;

public partial class I18nImpl
{
    /// <inheritdoc/>
    public async Task<Result<I18nSection>> ForSection(string sectionKey)
    {
        if (string.IsNullOrEmpty(sectionKey))
        {
            return "Cannot load i18n for an empty section key".AsResult<I18nSection>();
        }

        lock (_loadedSections)
        {
            if (_loadedSections.TryGetValue(sectionKey, out var section))
            {
                _logger.Debug?.Log($"Got i18n section from memory cache: {section}");
                return Result.Ok(section);
            }
        }

        var sw = Stopwatch.StartNew();
        var result = await LoadPristineSection(sectionKey);
        if (!result.IsOk(out var pristineSection, out var error))
        {
            _logger.Error?.Log(error);
            return error.AsResult<I18nSection>();
        }
        _logger.Info?.Log(
            $"Loaded pristine i18n section {sectionKey} in {sw.ElapsedMilliseconds:N2}ms"
        );

        lock (_loadedSections)
        {
            _loadedSections[sectionKey] = pristineSection;
        }
        return Result.Ok(pristineSection);
    }

    private async Task<Result<I18nSection>> LoadPristineSection(string sectionKey)
    {
        // TODO: get player locale (ex: "en_US", "pt_BR")
        var locale = "pt_BR";

        var fromResource = LoadFromResource(locale, sectionKey);
        if (fromResource.IsOk())
        {
            return fromResource;
        }

        var fromServer = await LoadFromServer(locale, sectionKey);
        if (fromServer.IsOk())
        {
            return fromServer;
        }

        return $"Could not find section {sectionKey}".AsResult<I18nSection>();
    }
}
