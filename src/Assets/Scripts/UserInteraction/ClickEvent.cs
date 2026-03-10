using UnityEngine;

public class ClickEvent
{
    /** Vetor de distância entre o click e o centro do objeto que foi clicado. */
    public Vector2 Offset { get; }

    public ClickEvent(Collider2D hit, Vector3 clickPos)
    {
        Offset = hit.transform.position - clickPos;
    }
}
