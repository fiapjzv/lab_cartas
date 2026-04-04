using System;
using System.Threading.Tasks;
using Game.Core.Utils;

public partial class I18nImpl
{
    /// <inheritdoc/>
    public async Task<Result<I18nSection>> ForSection(string sectionKey)
    {
        lock (_loadedSections)
        {
            if (_loadedSections.TryGetValue(sectionKey, out var section))
            {
                return Result.Ok(section);
            }
        }

        I18nSection pristineSection;
        try
        {
            pristineSection = await TryLoadSection(sectionKey);
        }
        catch (Exception ex)
        {
            return $"Failed to load {nameof(I18n)} section '{sectionKey}': {ex}".AsResult<I18nSection>();
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
        // TODO: load i18n from disk or server
        return new MockI18nSection();
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
