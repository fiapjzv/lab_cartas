public partial class GameSetup
{
    /// <summary>Testa conexão com o servidor.</summary>
    private async Task ConnectToServer()
    {
        // NOTE: essa espera de 2 segundos está simulando a conexão com o servidor
        await Task.Delay(2000);
        Debug.Log("Server connection stablished!");
    }
}
