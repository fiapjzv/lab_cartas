[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public partial class Clickable : MonoBehaviour
{
    /// <summary>
    /// Caso seja "true" o jogador pode interagir com o elemento.
    /// Caso "false" ela está no meio de uma animação ou algum outro estado em que não se pode interagir com ele.
    /// </summary>
    // private bool canInteract = true;

    // public void Click(ClickEvent click)
    // {
    //     if (!canInteract)
    //     {
    //         return;
    //     }
    // }
}
