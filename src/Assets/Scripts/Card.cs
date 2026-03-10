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

    public void ReleaseClick()
    {
        // 1. verificar se a posição do mouse está numa zona
        // 2. se não estiver => voltar pra zona anterior
        // 3. se estiver => verificar se pode jogar lá
        // 4. se não puder => voltar pra zona anterior
        // 5. se puder => colocar na nova zona
        dragging = null;
        Debug.Log($"Not dragging anymore: {this}");
    }

    private bool IsDragging()
    {
        return dragging is not null;
    }
}
