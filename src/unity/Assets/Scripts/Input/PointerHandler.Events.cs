using UnityEngine;

/// <summary>Evento disparado quando ocorre um clique com o ponteiro (mouse ou dedo).</summary>
/// <remarks>Exige que o objeto possua um component <see cref="IClickable" /></remarks>
public readonly struct PointerClickEvt
{
    /// <summary>Collider2D que foi atingido pelo clique</summary>
    public Collider2D Hit { get; }

    /// <summary>Posição do ponteiro no momento do clique (em coordenadas de mundo).</summary>
    public Vector3 PointerPos { get; }

    /// <inheritdoc cref="PointerClickEvt" />
    public PointerClickEvt(Collider2D hit, Vector3 pointerPos)
    {
        Hit = hit;
        PointerPos = pointerPos;
    }
}

/// <summary>Evento disparado durante o arraste do ponteiro "segurando" um objeto.</summary>
/// <remarks>Exige que o objeto possua um component <see cref="IDraggable" /></remarks>
public readonly struct PointerDragEvt
{
    /// <summary>GameObject sendo arrastado.</summary>
    public GameObject Target { get; }

    /// <summary>Posição atual do ponteiro durante o arraste (em coordenadas de mundo).</summary>
    public Vector3 PointerPos { get; }

    /// <inheritdoc cref="PointerDragEvt" />
    public PointerDragEvt(GameObject target, Vector3 pointerPos)
    {
        Target = target;
        PointerPos = pointerPos;
    }
}

/// <summary>Evento disparado quando o ponteiro é liberado (fim do clique ou drag).</summary>
public readonly struct PointerReleaseEvt
{
    /// <summary>GameObject que foi clicado.</summary>
    public GameObject Target { get; }

    /// <summary>Posição do ponteiro no momento da liberação (em coordenadas de mundo).</summary>
    public Vector3 PointerPos { get; }

    /// <summary>Indica se algum objeto estava sendo arrastado ou foi um clique simples</summary>
    public bool WasDragged { get; }

    /// <inheritdoc cref="PointerReleaseEvt" />
    public PointerReleaseEvt(GameObject target, Vector3 pointerPos, bool wasDragged)
    {
        Target = target;
        PointerPos = pointerPos;
        WasDragged = wasDragged;
    }
}
