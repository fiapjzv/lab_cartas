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
        Debug.LogWarning("TODO!");
        return true;
    }
}
