using Game.Core.Services;

public partial class I18nImpl
{
    /// <inheritdoc cref="I18n.Start" />
    public void Start(string[] mandatorySections)
    {
        foreach (var section in mandatorySections)
        {
            var result = ForSection(section).GetAwaiter().GetResult();
            // TODO: what should we do in case of error: Panic?!
            _logger.LogResult("Load I18n: {0}", result);
        }
    }
}
