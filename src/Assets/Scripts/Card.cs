using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public partial class Card : MonoBehaviour, IClickable
{
    private ClickEvent? dragging;

    /**
     * <inheritdoc cref="ICardZone"/>
     * <remarks>A carta deve ser retornada a sua área se o jogador tentar movê-la para uma área inválida.</remarks>
     * */
    private ICardZone? cardZone;

    public void Click(ClickEvent click)
    {
        dragging = click;
        Debug.Log($"Starting to drag: {this}");
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
            ReturnToPrevCardZone();
            return;
        }

        if (newZone.TryAdd(this))
        {
            cardZone = newZone;
        }
        else
        {
            ReturnToPrevCardZone();
        }

        dragging = null;
        Debug.Log($"Not dragging anymore: {this}");
    }

    private bool IsInCardZone(Vector2 pointerPos, [NotNullWhen(true)] out ICardZone newZone)
    {
        throw new NotImplementedException();
    }

    private void ReturnToPrevCardZone()
    {
        throw new NotImplementedException();
    }

    private bool IsDragging()
    {
        return dragging is not null;
    }
}
