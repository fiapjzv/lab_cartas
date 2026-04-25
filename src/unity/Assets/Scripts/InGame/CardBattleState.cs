using System.Collections.Generic;
using System.Linq;
using CardInstanceId = System.Guid;
using SorcererId = System.Guid;

/// <summary>Represents the snapshot of an active card battle.</summary>
public struct CardBattleState
{
    /// <summary>Unique ID of the player running the app.</summary>
    public SorcererId PlayerId { get; }

    /// <summary>Collection of state data for all sorcerers in the battle (including the player).</summary>
    public SorcererState[] SorcerersState { get; }

    /// <summary>Current turn index (zero based).</summary>
    public int TurnCount { get; set; }

    /// <summary>Slots of the player hand (might be empty).</summary>
    public CardInstanceId?[] CurrentPlayerHand { get; }

    public CardBattleState(SorcererId playerId, SorcererInBattle[] sorcerersInBattle)
    {
        PlayerId = playerId;
        SorcerersState = sorcerersInBattle.Select(SorcererState.Create).ToArray();
        CurrentPlayerHand = new CardInstanceId?[GameRules.MAX_CARDS_IN_SORCERER_HAND];
        TurnCount = 0;
    }
}

/// <summary>A sorcerer entering in a battle.</summary>
public struct SorcererInBattle
{
    /// <inheritdoc cref="SorcererState.SorcererId"/>
    public SorcererId SorcererId { get; }

    /// <inheritdoc cref="AuraState.AuraBet"/>
    public int AuraBet { get; }

    /// <inheritdoc cref="SorcererInBattle"/>
    public SorcererInBattle(SorcererId sorcererId, int auraBet)
    {
        SorcererId = sorcererId;
        AuraBet = auraBet;
    }
}

/// <summary>Tracks current sorcerer state on the battle.</summary>
public struct SorcererState
{
    /// <summary>Unique ID of the player (might be the current player).</summary>
    public SorcererId SorcererId { get; }

    /// <inheritdoc cref="AuraState"/>
    public AuraState CurrAura { get; }

    /// <summary>How many cards left on the sorcerer's deck.</summary>
    public int DeckCardsCount { get; set; }

    /// <summary>How many cards are currently on the sorcerer's hand.</summary>
    public int HandCardsCount { get; set; }

    /// <summary>Cards that have been discarded or destroyed.</summary>
    public List<CardInstanceId> Graveyard { get; }

    /// <summary> Cards currently active on the battlefield/board. </summary>
    public List<BattlefieldCardState> CardsInBattlefield { get; set; }

    /// <inheritdoc cref="SorcererState"/>
    public static SorcererState Create(SorcererInBattle sorcerer) => new(sorcerer);

    private SorcererState(SorcererInBattle sorcerer)
    {
        SorcererId = sorcerer.SorcererId;
        CurrAura = new AuraState(sorcerer.AuraBet);
        Graveyard = new List<CardInstanceId>();
        CardsInBattlefield = new List<BattlefieldCardState>();
        DeckCardsCount = GameRules.MAX_CARDS_IN_DECK;
        HandCardsCount = 0;
    }
}

/// <summary>State related to the aura counters of a sorcerer on a battle.</summary>
public struct AuraState
{
    /// <summary>The aura stakes on the battle (how much </summary>
    public int AuraBet { get; }

    /// <summary>Current aura of the player.</summary>
    public int CurrAura { get; set; }

    /// <inheritdoc cref="AuraState"/>
    public AuraState(int auraBet)
    {
        AuraBet = auraBet;
        CurrAura = auraBet;
    }
}
