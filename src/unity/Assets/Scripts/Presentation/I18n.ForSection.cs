using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Game.Core.Utils;

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

        I18nSection pristineSection;
        try
        {
            var sw = Stopwatch.StartNew();
            pristineSection = await TryLoadSection(sectionKey);
            _logger.Info?.Log(
                $"Loaded pristine i18n section {sectionKey} in {sw.ElapsedMilliseconds:N2}ms"
            );
        }
        catch (Exception ex)
        {
            var error = $"Failed to load {nameof(I18n)} section '{sectionKey}': {ex}";
            _logger.Error?.Log(error);
            return error.AsResult<I18nSection>();
        }

        lock (_loadedSections)
        {
            if (!_loadedSections.ContainsKey(sectionKey))
            {
                _loadedSections[sectionKey] = pristineSection;
            }

            var section = _loadedSections[sectionKey];
            return Result.Ok(section);
        }
    }

    private async Task<I18nSection> TryLoadSection(string sectionKey)
    {
        // TODO: load i18n from local disk
        // TODO: load i18n from server
        var i18nSection = new MockI18nSection();
        return i18nSection;
    }
}

internal class MockI18nSection : I18nSection
{
    public string Key => "mock";

    public string Label(string i18nKey)
    {
        return i18nKey switch
        {
            "story-btn" => "STORY",
            "versus-btn" => "VERSUS",
            "versus-btn.subtext" => "SOON",
            "collection-btn" => "COLLECTION",
            "settings-btn" => "SETTINGS",
            "quit-btn" => "QUIT",
            _ => $"[{i18nKey}]",
        };
    }

    public override string ToString() => Key;
}
