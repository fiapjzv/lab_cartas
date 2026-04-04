using Game.Core.Services;

public partial class I18nImpl
{
    /// <inheritdoc cref="I18n.Start" />
    public void Start(string[] mandatorySections)
    {
        foreach (var sectionKey in mandatorySections)
        {
            var result = TryLoadSectionSync(sectionKey);
            // TODO: what should we do in case of error: Panic?!
            _logger.LogResult("Load I18n resourece: {0}", result);
        }
    }
}
