using System.Threading.Tasks;
using Game.Core.Services;

public partial class GameManager
{
    /// <summary>Testa conexão com o servidor.</summary>
    private static async Task TestServerConn(IGameLogger logger)
    {
        // NOTE: essa espera de 2 segundos está simulando a conexão com o servidor
        await 2.Seconds().Delay();
        logger.Debug?.Log("Server connection established!");
    }
}
