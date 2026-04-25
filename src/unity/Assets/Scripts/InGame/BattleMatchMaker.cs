public interface IBattleMatchMaker
{
    IBattleMatch CurrMatch();
}

public interface IBattleMatch
{
    SorcererInBattle[] Sorcerers();
}

public class BattleMatchMaker : IBattleMatchMaker
{
    public IBattleMatch CurrMatch()
    {
        throw new System.NotImplementedException();
    }
}
