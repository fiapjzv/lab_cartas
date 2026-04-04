using System.Collections.Generic;
using Game.Core.Utils;

/// <summary>Representa um agrupamento de chaves (ex: "menu").</summary>
public interface I18nSection
{
    /// <summary>Identificador da seção. Exemplo: "menu", "enemy-cards", "global"</summary>
    string Key { get; }

    int LabelsCount();

    /// <summary>Tradução do texto na língua do jogador (se disponível).</summary>
    Result<string> Label(string i18nKey);
}

public class I18nSectionImpl : I18nSection
{
    private readonly Dictionary<string, string> _labels = new();

    public string Key { get; }

    public I18nSectionImpl(string key)
    {
        Key = key;
    }

    public void Add(string key, string translation) => _labels[key] = translation;

    public Result<string> Label(string i18nKey)
    {
        if (_labels.TryGetValue(i18nKey, out var label))
        {
            return Result.Ok(label);
        }

        return $"Could not find i18n label for {i18nKey} on section {Key}".AsResult<string>();
    }

    public int LabelsCount() => _labels.Count;
}
