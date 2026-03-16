using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public partial class Card : MonoBehaviour, IClickable
{
    /// <inheritdoc cref="ClickEvent"/>
    /// <remarks>Vai ser "null" caso não estejamos arrastando a carta.</remarks>
    private ClickEvent? dragging;

    /// <inheritdoc cref="ICardZone"/>
    /// <remarks>A carta deve ser retornada a sua área se o jogador tentar movê-la para uma área inválida.</remarks>
    private ICardZone? cardZone;

    /**
     * Armazena a última posição da carta, para que possamos voltar a ela caso o jogador não coloque a carta em
     * uma posição válida.
     * */
    private Vector3 lastPos;

    public void Click(ClickEvent click)
    {
        dragging = click;
        lastPos = transform.position;
        Debug.Log($"Starting to drag: {this} from {lastPos}");
    }

    public void DragTo(Vector2 position)
    {
        if (!IsDragging())
        {
            Debug.LogError($"Card {nameof(DragTo)} called when not dragging");
            return;
        }

        transform.position = position + dragging.Offset;
    }

    public void ReleaseClick(Vector2 pointerPos)
    {
        if (!IsInCardZone(pointerPos, out ICardZone newZone))
        {
            MoveToPrevCardZone();
            return;
        }

        if (newZone.TryAdd(this))
        {
            cardZone = newZone;
            lastPos = transform.position;
        }
        else
        {
            MoveToPrevCardZone();
        }

        dragging = null;
        Debug.Log($"Not dragging anymore: {this}");
    }

    private bool IsInCardZone(Vector2 pointerPos, [NotNullWhen(true)] out ICardZone? newZone)
    {
        var hits = Physics2D.OverlapPointAll(pointerPos);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<ICardZone>(out newZone))
            {
                Debug.Log($"Card released inside card zone {newZone}");
                return true;
            }
        }
        newZone = null;
        Debug.Log("Card released outsize card zones");
        return false;
    }

    private void MoveToPrevCardZone()
    {
        transform.position = lastPos;
        Debug.LogWarning($"Moving card back to {lastPos}");
    }

    private bool IsDragging()
    {
        return dragging is not null;
    }
}
