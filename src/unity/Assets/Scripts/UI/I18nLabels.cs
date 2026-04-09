using System.Threading.Tasks;
using Game.Core.Services;
using UnityEngine;
using UnityEngine.UIElements;

public partial class I18nLabels : MonoBehaviour
{
    private IGameLogger _logger = null!;
    private I18n _i18n = null!;
    private VisualElement _root = null!;

    [field: Header("MANDATORY: Every label on this screen must be on this section.")]
    [field: SerializeField]
    public string I18nSection { get; set; } = null!;

    private bool _alreadyTranslated;

    public void Awake()
    {
        _logger = Service.Get<IGameLogger>();
        _i18n = Service.Get<I18n>();

        Guard.NotNull(I18nSection, _logger);
        var uiDocument = Guard.NotNull(GetComponent<UIDocument>(), _logger);
        _root = uiDocument.rootVisualElement;
    }

    public void OnEnable()
    {
        if (_alreadyTranslated)
        {
            return;
        }

        _ = AsyncLoadI18nSection();

        _alreadyTranslated = true;
    }

    private async Task AsyncLoadI18nSection()
    {
        var i18nSection = await _i18n.ForSection(I18nSection);
        if (!i18nSection.IsOk(out var section, out var error))
        {
            _logger.Error?.Log($"Could not find i18n section {i18nSection} on screen {gameObject.name}: {error}");
            return;
        }

        foreach (var label in _root.Query<TextElement>().ToList())
        {
            var text = label.text;
            // NOTE: text might be empty for nested elements (ex. a Button with a Label inside)
            if (string.IsNullOrEmpty(text))
            {
                continue;
            }

            var result = section.Label(text);
            if (result.IsOk(out var translated, out error))
            {
                _logger.Debug?.Log($"Got translation for {text}: {translated}");
                label.text = translated;
            }
            else
            {
                label.text = $"[{text}]";
                _logger.Error?.Log(error);
            }
        }
    }
}
