/// <summary>Gerencia o ponteiro do jogo: mouse ou dedo no celular.</summary>
public partial class PointerHandler : MonoBehaviour
{
    private Camera _cam = null!;
    private IGameLogger _logger = null!;
    private IEvents _events = null!;

    private GameObject? _currClicked;

    void Awake()
    {
        _cam = Camera.main;
        _logger = Service.Get<IGameLogger>();
        _events = Service.Get<IEvents>();
    }
}
