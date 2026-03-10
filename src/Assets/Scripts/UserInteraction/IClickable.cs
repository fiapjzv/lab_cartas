using UnityEngine;

public interface IClickable
{
    void Click(ClickEvent click);
    void ReleaseClick();
    void DragTo(Vector2 pointerPos);
}
