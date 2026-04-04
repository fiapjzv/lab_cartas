/// <summary>Representa um agrupamento de chaves (ex: "menu").</summary>
public interface I18nSection
{
    /// <summary>Identificador da seção. Exemplo: "menu", "enemy-cards", "global"</summary>
    string Key { get; }

    /// <summary>Tradução do texto na língua do jogador.</summary>
    string Label(string i18nKey);
}
