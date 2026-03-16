using UnityEngine;

public partial class TableZone : MonoBehaviour, ICardZone
{
    void Awake()
    {
        var sprite = GetComponent<SpriteRenderer>();
        sprite.sortingOrder = SpriteSortOrder.CARD_ZONE;
    }

    public bool TryAdd(Card card)
    {
        return true;
    }

    public void RemoveCard(Card card) { }
}
