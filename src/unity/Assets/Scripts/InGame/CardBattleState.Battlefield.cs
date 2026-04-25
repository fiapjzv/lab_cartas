using System.Collections.Generic;
using CardInstanceId = System.Guid;

/// <summary>State of the cards currently in play.</summary>
public struct BattlefieldCardState
{
    public CardInstanceId Card { get; }

    /// <summary>List of active modifiers affecting this card instance.</summary>
    public List<CardModifier> Modifiers { get; }

    /// <inheritdoc cref="BattlefieldCardState"/>
    public BattlefieldCardState(CardInstanceId card)
    {
        Card = card;
        // PERF: normally a card will have 0-1 modifiers, avoiding the extra allocations)
        Modifiers = new List<CardModifier>(capacity: 1);
    }
}

/// <summary>Data container for an effect applied to a card, tracking its source.</summary>
public struct CardModifier
{
    /// <summary>The instance ID of the card that originated this modifier.</summary>
    public CardInstanceId SourceCard { get; set; }

    /// <summary> The collection of specific effects this modifier provides. </summary>
    public List<IModifierEffect> ModifierEffects { get; set; }
}

/// <summary>How a modifier changes a card's state.</summary>
public interface IModifierEffect
{
    /// <summary>Applies the specific effect logic to the target card.</summary>
    void Apply(CardInstanceId cardInstanceId);
}
