public partial class GameSetup
{
    /// <summary>Display main menu.</summary>
    private void ShowMenu(IGameLogger logger)
    {
        logger.Info?.Log("Main menu rendered");
    }

    private enum MenuItem
    {
        QuickMatch,
        StoryMode,
        Settings,
    }
}
