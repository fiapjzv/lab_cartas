using UnityEngine;

public interface IClickable
{
    void Click(ClickEvent click);
    void DragTo(Vector2 position);
    void ReleaseClick();
}
