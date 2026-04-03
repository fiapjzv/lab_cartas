using Game.Core.Services;
using UnityEngine;
using UnityEngine.UIElements;

public partial class I18nLabels : MonoBehaviour
{
    private IGameLogger _logger = null!;
    private I18N _i18n = null!;
    private VisualElement _root = null!;

    public void Awake()
    {
        var uiDocument = Guard.NotNull(GetComponent<UIDocument>(), _logger);
        _root = uiDocument.rootVisualElement;

        _logger = Service.Get<IGameLogger>();
        _i18n = Service.Get<I18N>();
    }

    // public async Task OnEnable()
    // {
    //     var i18nSection = await _i18n.ForSection(I18nSection);
    //     foreach (var label in _root.Query<Label>().ToList())
    //     {
    //         label.text = i18nSection.Localize(label.text);
    //     }
    // }
}
