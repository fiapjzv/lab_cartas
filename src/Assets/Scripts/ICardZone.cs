///<summary>Área onde uma carta pode estar: na mão do jogador, pilha de descartes, mesa, etc.</summary>
public interface ICardZone
{
    ///<summary>Jogador tentou mover a carta <paramref name="card"/> pra dentro dessa área.</summary>
    Result TryAdd(Card card);
}
