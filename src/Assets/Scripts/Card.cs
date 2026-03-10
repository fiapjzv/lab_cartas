using UnityEngine;

public partial class Card : MonoBehaviour, IClickable
{
    private ClickEvent? dragging;

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

    public void ReleaseClick()
    {
        dragging = null;
        Debug.Log($"Not dragging anymore: {this}");
    }

    private bool IsDragging()
    {
        return dragging is not null;
    }
}
