using System.Threading.Tasks;
using Game.Core.Services;

public partial class GameSetup
{
    /// <summary>Testa conexão com o servidor.</summary>
    private async Task TestServerConn(IGameLogger logger)
    {
        // NOTE: essa espera de 2 segundos está simulando a conexão com o servidor
        await Task.Delay(2000);
        logger.Debug?.Log("Server connection stablished!");
    }
}
