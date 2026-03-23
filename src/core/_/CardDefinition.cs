namespace Game.Core;

public class CardDefinition
{
    public string Name { get; private set; }

    public CardDefinition(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return $"{nameof(CardDefinition)} {Name}";
    }
}
