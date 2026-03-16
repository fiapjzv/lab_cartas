using UnityEngine;

public interface IClickable
{
    void Click(ClickEvent click);
    void DragTo(Vector2 pointerPos);
    void ReleaseClick(Vector2 pointerPos);
}
