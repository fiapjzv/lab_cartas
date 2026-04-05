using System.Diagnostics;
using System.Threading.Tasks;
using Game.Core.UI;
using Game.Core.Utils;
using UnityEngine;

public partial class I18nImpl
{
    private Result<I18nSection> TryLoadFromResourceAndCache(string sectionKey)
    {
        var locale = Lang.ToLocaleCode();
        var resourcePath = $"I18n/{sectionKey}/{locale}";
        var sw = Stopwatch.StartNew();
        var textAsset = Resources.Load<TextAsset>(resourcePath);

        if (textAsset is null)
        {
            return $"Could not find Resource locally on {resourcePath}".AsResult<I18nSection>();
        }

        var result = ParseCsv(textAsset.text, sectionKey, locale);
        // NOTE: freeing up memory
        Resources.UnloadAsset(textAsset);

        if (!result.IsOk(out var pristineSection, out var error))
        {
            _logger.Error?.Log(error);
            return error.AsResult<I18nSection>();
        }

        AddIntoCache(pristineSection);
        _logger.Info?.Log(
            $"Loaded pristine i18n section {sectionKey} in {sw.ElapsedMilliseconds:N2}ms"
        );

        return Result.Ok(pristineSection);
    }

    private async Task<Result<I18nSection>> TryLoadFromServerAndCache(string _)
    {
        await 100.Millis().Delay();
        return "I18n from server is not implemented yet.".AsResult<I18nSection>();
    }
}
