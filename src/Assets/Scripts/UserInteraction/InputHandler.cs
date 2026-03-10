using UnityEngine;
using UnityEngine.InputSystem;

public partial class ClickHandler : MonoBehaviour
{
    private Camera cam;
    private IClickable? currClicked;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        var pointer = Pointer.current;
        if (pointer == null)
        {
            Debug.LogError("No pointer found!");
            return;
        }

        if (pointer.press.wasPressedThisFrame)
        {
            var clickPos = cam.ScreenToWorldPoint(pointer.position.ReadValue());
            var hit = Physics2D.OverlapPoint(clickPos);

            if (hit is null)
            {
                Debug.Log($"Player clicked on nothing");
                return;
            }

            Debug.Log($"Player cliked on {hit.gameObject}");
            var clickable = hit.GetComponent<IClickable>();
            if (clickable is null)
            {
                Debug.Log($"Clicked object is not {nameof(IClickable)}");
                return;
            }

            currClicked = clickable;
            clickable.Click(new ClickEvent(hit, clickPos));
            return;
        }

        if (currClicked is null)
        {
            return;
        }

        if (pointer.press.isPressed)
        {
            var mousePos = cam.ScreenToWorldPoint(pointer.position.ReadValue());
            currClicked.DragTo(mousePos);
        }

        if (pointer.press.wasReleasedThisFrame)
        {
            currClicked.ReleaseClick();
            currClicked = null;
        }
    }
}
