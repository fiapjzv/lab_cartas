using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>Gerencia o ponteiro do jogo: mouse ou dedo no celular.</summary>
public partial class PointerHandler : GameBehavior
{
    private Camera _cam = null!;
    private Pointer _pointer = null!;

    private GameObject? _currSelected;
    private Vector3? _currClickPos;
    private bool _isDragging;

    protected override void Init()
    {
        _cam = Guard.NotNull(Camera.main, Logger);
        // NOTE: se o usuário desconectar o mouse e conectar outro vai parar de funcionar
        _pointer = Guard.NotNull(Pointer.current, Logger);
    }
}
