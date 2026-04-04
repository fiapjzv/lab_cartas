using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Game.Core.Utils;

public partial class I18nImpl
{
    /// <inheritdoc/>
    public async Task<Result<I18nSection>> ForSection(string sectionKey)
    {
        var result = TryLoadSectionSync(sectionKey);
        if (result.IsOk())
        {
            return result;
        }

        return await TryLoadFromServerAndCache(sectionKey);
    }

    private Result<I18nSection> TryLoadSectionSync(string sectionKey)
    {
        Guard.NotEmpty(sectionKey, _logger);

        if (TryGetFromCache(sectionKey, out var section))
        {
            _logger.Debug?.Log($"Got i18n section from memory cache: {section}");
            return Result.Ok(section);
        }

        return TryLoadFromResourceAndCache(sectionKey);
    }

    private bool TryGetFromCache(
        string sectionKey,
        [NotNullWhen(returnValue: true)] out I18nSection? section
    )
    {
        lock (_loadedSections)
        {
            if (_loadedSections.TryGetValue(sectionKey, out section))
            {
                _logger.Debug?.Log($"Got i18n section from memory cache: {section}");
                return true;
            }
        }
        return false;
    }

    private void AddIntoCache(I18nSection pristineSection)
    {
        lock (_loadedSections)
        {
            _loadedSections[pristineSection.Key] = pristineSection;
        }
    }

    private Result<I18nSection> ParseCsv(string csv, string sectionKey, string locale)
    {
        var section = new I18nSectionImpl(sectionKey);

        var reader = new StringReader(csv);

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                _logger.Warn?.Log($"Empty line on I18N section file: {sectionKey}@{locale}");
                continue;
            }

            var span = line.AsSpan();
            var firstSeparator = span.IndexOf(';');
            if (firstSeparator < 0)
            {
                _logger.Warn?.Log($"Invalid line on I18N section file: {sectionKey}@{locale}");
                continue;
            }

            var key = span[..firstSeparator].ToString();
            var translation = span[(firstSeparator + 1)..].ToString();
            section.Add(key, translation);
        }

        if (section.LabelsCount() == 0)
        {
            return $"Could not find any valid translations on {sectionKey}@{locale}".AsResult<I18nSection>();
        }

        _logger.Info?.Log(
            $"Loaded {section.LabelsCount()} labels from Resource csv: {sectionKey}@{locale}"
        );

        return Result.Ok((I18nSection)section);
    }
}
