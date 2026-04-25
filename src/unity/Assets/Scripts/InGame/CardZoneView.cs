using UnityEngine;

/// <summary>An area where a card can be located. See <see cref="CardZoneView.Type"/></summary>
public interface ICardZone
{
    /// <inheritdoc cref="CardZoneView.Type"/>
    CardZoneView.Type ZoneType { get; }
}

/// <inheritdoc cref="ICardZone" />
public partial class CardZoneView : GameBehavior, ICardZone
{
    [field: SerializeField]
    public Type ZoneType { get; private set; }

    private BoxCollider2D _boxCollider = null!;

    protected override void Init()
    {
        Premisses.ValidateCamera();
        SetupCollider();
    }

    /// <summary>The type of zone on the screen where you can hold cards.</summary>
    public enum Type
    {
        /// <summary>Cards currently held by the player.</summary>
        PlayerHand,

        /// <summary>The player's draw pile.</summary>
        PlayerDeck,

        /// <summary>Cards that are in game generating effects and aura to the player.</summary>
        BattlefieldArea,

        /// <summary>The pile for used, destroyed or discarded cards.</summary>
        Graveyard,
    }
}
