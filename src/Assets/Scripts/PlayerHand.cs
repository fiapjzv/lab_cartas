using System.Collections.Generic;
using UnityEngine;

public partial class PlayerHand : MonoBehaviour, ICardZone
{
    private readonly List<Card> cards = new();

    public bool TryAdd(Card card)
    {
        if (cards.Count >= MAX_CARDS)
        {
            return false;
        }

        // BUG: se colocamos uma carta aqui, tiramos e colocamos novamente ela vai estar 2x na mesma lista
        cards.Add(card);
        Debug.Log($"Cards in hand: {cards.Count}");
        return true;
    }

    private const int MAX_CARDS = 5;
}
