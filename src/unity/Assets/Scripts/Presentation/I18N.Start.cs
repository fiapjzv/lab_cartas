using Game.Core.Services;

public partial class I18NImpl
{
    /// <inheritdoc cref="I18N.Start" />
    public void Start(string[] mandatorySections)
    {
        foreach (var section in mandatorySections)
        {
            var result = ForSection(section).GetAwaiter().GetResult();
            // TODO: what should we do in case of error: Panic?!
            _logger.LogResult("Load I18N: {0}", result);
        }
    }
}
