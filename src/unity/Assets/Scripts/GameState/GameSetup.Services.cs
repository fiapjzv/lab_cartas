using Game.Core.Services;

public partial class GameSetup
{
    /// <summary>Instancia serviços básicos para o funcionamento de tudo.</summary>
    private void SetupServices()
    {
        events = Events.Instance;
        Debug.Log("Setup services complete");
    }
}
