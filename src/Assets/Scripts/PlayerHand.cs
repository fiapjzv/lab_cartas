using System.Collections.Generic;
using UnityEngine;

public partial class PlayerHand : MonoBehaviour, ICardZone
{
    private readonly List<Card> cards = new();

    void Awake()
    {
        var sprite = GetComponent<SpriteRenderer>();
        sprite.sortingOrder = SpriteSortOrder.CARD_ZONE;
    }

    public bool TryAdd(Card card)
    {
        if (cards.Count >= MAX_CARDS)
        {
            return false;
        }

        cards.Add(card);
        Debug.Log($"Cards in hand: {cards.Count}");
        return true;
    }

    public void RemoveCard(Card card)
    {
        cards.Remove(card);
        Debug.Log($"Cards in hand: {cards.Count}");
    }

    private const int MAX_CARDS = 5;
}
