/// <summary>
/// Centraliza a definição de ordem de renderização dos sprites via código,
/// evitando o uso de "magic numbers" no editor do Unity.
///
/// Benefícios:
/// - Garante consistência global (todos usam os mesmos valores).
/// - Facilita manutenção e refatoração (alteração em um único lugar).
/// - Melhora legibilidade ao substituir números por nomes.
/// - Reduz erros humanos ao configurar manualmente no editor.
/// - Permite versionamento e revisão dessas regras junto com o código.
/// </summary>
public static class SpriteSortOrder
{
    /// <summary>Fundo do jogo (background estático)</summary>
    /// <inheritdoc cref="SpriteSortOrder" />
    public const int BG = 0;

    /// <summary>Camada intermediária simples (midground sem sobreposição crítica)</summary>
    /// <inheritdoc cref="SpriteSortOrder" />
    public const int MG = 50;

    /// <summary>Áreas onde cartas podem ser posicionadas.</summary>
    /// <inheritdoc cref="SpriteSortOrder" />
    public const int CARD_ZONE = 100;

    /// <summary>Cartas em jogo (em repouso)</summary>
    /// <inheritdoc cref="SpriteSortOrder" />
    public const int CARD = 200;

    /// <summary>Cartas na mão do jogador</summary>
    /// <inheritdoc cref="SpriteSortOrder" />
    public const int CARD_IN_HAND = 250;

    /// <summary>Cartas sendo movidas (renderizado sobre as cartas que estão em repouso)</summary>
    /// <inheritdoc cref="SpriteSortOrder" />
    public const int CARD_MOVING = 270;

    /// <summary>Efeitos visuais (partículas, highlights, animações)</summary>
    /// <inheritdoc cref="SpriteSortOrder" />
    public const int EFFECTS = 300;

    /// <summary>Interface de usuário (sempre no topo).</summary>
    /// <inheritdoc cref="SpriteSortOrder" />
    public const int UI = 1000;
}
