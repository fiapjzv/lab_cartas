using System;

/// <summary>
/// Representa os dados de uma carta no jogo.
/// Essa classe precisa ser um objeto simples para poder ser serializada, enviada e recebida pelo cliente ou servidor.
/// </summary>
/// <remarks>
/// Usando uma classe na aplicação cliente para garantir que ela funcione com o serializador de Json do Unity,
/// apesar de ser a opção com menos performance.
/// </remarks>
public class CardData
{
    /// <summary>Id único de uma carta.</summary>
    public Guid Id { get; set; }

    /// <summary>Título da carta.</summary>
    public string Name { get; set; } = null!;

    /// <summary>Texto da carta com notação de formatação.</summary>
    public string Text { get; set; } = null!;

    /// <summary>Custo em aura para baixar uma carta.</summary>
    public int AuraCost { get; set; }

    /// <summary>A chave do sprite a ser exibido na camada principal da carta.</summary>
    public string FgSpriteKey { get; set; } = null!;

    /// <summary>A chave do sprite a ser exibido no fundo da carta.</summary>
    public string BgSpriteKey { get; set; } = null!;
}

public partial class CardView
{
    /// <summary>Altera a informação necessária para exibir os dados de uma carta.</summary>
    public void SetCardData(CardData cardData)
    {
        _cardData = cardData;
    }
}
