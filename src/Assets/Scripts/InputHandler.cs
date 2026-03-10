using UnityEngine;
using UnityEngine.InputSystem;

public partial class ClickHandler : MonoBehaviour
{
    private Camera cam;

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
        }
    }
}
