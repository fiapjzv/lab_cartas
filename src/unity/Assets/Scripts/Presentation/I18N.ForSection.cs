using System;
using System.Threading.Tasks;
using Game.Core.Utils;

public partial class I18NImpl
{
    /// <inheritdoc/>
    public async Task<Result<I18NSection>> ForSection(string sectionKey)
    {
        lock (_loadedSections)
        {
            if (_loadedSections.TryGetValue(sectionKey, out var section))
            {
                return Result.Ok(section);
            }
        }

        I18NSection pristineSection;
        try
        {
            pristineSection = await TryLoadSection(sectionKey);
        }
        catch (Exception ex)
        {
            return $"Failed to load I18N section '{sectionKey}': {ex}".AsResult<I18NSection>();
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

    public override string ToString() => Key;
}
