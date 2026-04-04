using System;
using System.IO;
using Game.Core.Utils;
using UnityEngine;

public partial class I18nImpl
{
    private Result<I18nSection> LoadFromResource(string locale, string sectionKey)
    {
        var resourcePath = $"I18n/{sectionKey}/{locale}";
        var textAsset = Resources.Load<TextAsset>(resourcePath);

        if (textAsset is null)
        {
            return $"Could not find Resource locally on {resourcePath}".AsResult<I18nSection>();
        }

        var result = ParseCsv(textAsset.text, sectionKey, locale);
        // NOTE: freeing up memory
        Resources.UnloadAsset(textAsset);
        return result;
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
