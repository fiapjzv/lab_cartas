using System;
using System.Threading.Tasks;

public partial class I18NImpl
{
    /// <inheritdoc/>
    public async Task<I18NSection> ForSection(string sectionKey)
    {
        lock (_loadedSections)
        {
            if (_loadedSections.TryGetValue(sectionKey, out var section))
            {
                return section;
            }
        }

        I18NSection pristineSection;
        try
        {
            pristineSection = await TryLoadSection(sectionKey);
        }
        catch (Exception ex)
        {
            _logger.Error?.Log($"Failed to load I18N section '{sectionKey}': {ex}");
            throw;
        }

        lock (_loadedSections)
        {
            if (!_loadedSections.ContainsKey(sectionKey))
            {
                _loadedSections[sectionKey] = pristineSection;
            }

            return _loadedSections[sectionKey];
        }
    }

    private async Task<I18NSection> TryLoadSection(string sectionKey)
    {
        // TODO: load i18n from disk or server
        return new MockI18NSection();
    }
}

internal class MockI18NSection : I18NSection
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
}
