namespace Game.Core.UI;

/// <summary>Línguas suportadas pelo jogo.</summary>
public enum Lang
{
    /// <summary>Português do Brasil.</summary>
    PT_BR,

    /// <summary>Inglês.</summary>
    EN,

    /// <summary>Espanhol.</summary>
    ES,

    /// <summary>Japonês.</summary>
    JA,

    /// <summary>Chinês simplificado (Hans).</summary>
    CH_HZ,
}

public static class LangExtensions
{
    /// <summary>Retorna o código de locale no padrão BCP 47 para cada idioma.</summary>
    public static string ToLocaleCode(this Lang lang)
    {
        return lang switch
        {
            Lang.PT_BR => "pt-BR",
            Lang.EN => "en",
            Lang.ES => "es",
            Lang.JA => "ja",
            Lang.CH_HZ => "zh-Hans",
            _ => throw new ArgumentOutOfRangeException(nameof(lang), lang, null),
        };
    }
}
