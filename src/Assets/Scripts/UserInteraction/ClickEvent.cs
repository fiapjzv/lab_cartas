using UnityEngine;

///<summary>Evento que marca o clique em uma carta.</summary>
public class ClickEvent
{
    ///<summary>Vetor de distância entre o click e o centro do objeto que foi clicado.</summary>
    public Vector2 Offset { get; }

    public ClickEvent(Collider2D hit, Vector3 pointerPos)
    {
        Offset = hit.transform.position - pointerPos;
    }
}
