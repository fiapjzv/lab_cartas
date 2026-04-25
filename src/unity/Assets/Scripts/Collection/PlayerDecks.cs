using System;

public interface IPlayerDecks
{
    IDeck CurrDeck();
}

public interface IDeck
{
    public Guid Id { get; }
}

public class PlayerDecks : IPlayerDecks
{
    public IDeck CurrDeck()
    {
        throw new NotImplementedException();
    }
}
