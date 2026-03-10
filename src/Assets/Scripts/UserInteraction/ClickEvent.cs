using UnityEngine;

public class ClickEvent
{
    ///<summary>Vetor de distância entre o click e o centro do objeto que foi clicado.</summary>
    public Vector2 Offset { get; }

    public ClickEvent(Collider2D hit, Vector3 clickPos)
    {
        Offset = hit.transform.position - clickPos;
    }
}
