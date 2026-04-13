using UnityEngine;

public partial class CardView
{
    private Vector3? _dragOffset;

    private void DragCard(PointerDragEvt evt)
    {
        if (evt.Target != gameObject)
        {
            return;
        }

        _dragOffset ??= transform.position - evt.PointerPos;
        transform.position = evt.PointerPos + _dragOffset.Value;
    }
}
