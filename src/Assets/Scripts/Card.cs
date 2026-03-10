using UnityEngine;

public partial class Card : MonoBehaviour, IClickable
{
    private bool isDragging;
    private Vector2 draggingOffset;

    public void Click(ClickEvent click)
    {
        isDragging = true;
        draggingOffset = click.Offset;
        Debug.Log($"Starting to drag: {this}");
    }

    public void DragTo(Vector2 position)
    {
        if (!isDragging)
        {
            Debug.LogError($"Card {nameof(DragTo)} called when not dragging");
            return;
        }

        transform.position = position + draggingOffset;
    }

    public void ReleaseClick()
    {
        isDragging = false;
        Debug.Log($"Not dragging anymore: {this}");
    }
}
