using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public partial class Card : MonoBehaviour, IClickable
{
    /// <inheritdoc cref="ClickEvent"/>
    /// <remarks>Vai ser "null" caso não estejamos arrastando a carta.</remarks>
    private ClickEvent? dragging;

    /// <inheritdoc cref="ICardZone"/>
    /// <remarks>A carta deve ser retornada a sua área se o jogador tentar movê-la para uma área inválida.</remarks>
    private ICardZone? cardZone;

    /// <summary>
    /// Armazena a última posição da carta, para que possamos voltar a ela caso o jogador não coloque a carta em
    /// uma posição válida.
    /// </summary>
    private Vector3 lastPos;

    /// <summary>
    /// Caso seja "true" o jogador pode interagir com a carta.
    /// Caso "false" ela está no meio de uma animação ou algum outro estado em que não se pode interagir com ela.
    /// </summary>
    private bool canInteract = true;

    /// <inheritdoc cref="RollbackCardMove"/>
    private RollbackCardMove? rollbackCardMove;

    void Awake()
    {
        var sprite = GetComponent<SpriteRenderer>();
        sprite.sortingOrder = SpriteSortOrder.CARD;
    }

    void Update()
    {
        rollbackCardMove?.ApplyRollbackMove(this, lastPos);
    }

    public void Click(ClickEvent click)
    {
        if (!canInteract)
        {
            return;
        }

        dragging = click;
        lastPos = transform.position;
        Debug.Log($"Starting to drag: {this} from {lastPos}");
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

    public void ReleaseClick(Vector2 pointerPos)
    {
        if (!IsInCardZone(pointerPos, out ICardZone newZone))
        {
            MoveToPrevCardZone();
            return;
        }

        if (newZone.TryAdd(this))
        {
            ChangeCardZone(newZone);
            lastPos = transform.position;
        }
        else
        {
            MoveToPrevCardZone();
        }

        dragging = null;
        Debug.Log($"Not dragging anymore: {this}");
    }

    public void ChangeCardZone(ICardZone cardZone)
    {
        if (this.cardZone is not null)
        {
            this.cardZone.RemoveCard(this);
        }

        this.cardZone = cardZone;
    }

    private bool IsInCardZone(Vector2 pointerPos, [NotNullWhen(true)] out ICardZone? newZone)
    {
        var hits = Physics2D.OverlapPointAll(pointerPos);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<ICardZone>(out newZone))
            {
                Debug.Log($"Card released inside card zone {newZone}");
                return true;
            }
        }
        newZone = null;
        Debug.Log("Card released outsize card zones");
        return false;
    }

    private void MoveToPrevCardZone()
    {
        rollbackCardMove = new RollbackCardMove();
        StartAnimation();
        Debug.LogWarning($"Starting to move card back to {lastPos}");
    }

    public void StartAnimation()
    {
        canInteract = false;
        Debug.Log("Starting animation");
    }

    public void EndAnimation()
    {
        Debug.Log("Ending animation");
        canInteract = true;
        rollbackCardMove = null;
    }

    private bool IsDragging()
    {
        return dragging is not null;
    }
}
