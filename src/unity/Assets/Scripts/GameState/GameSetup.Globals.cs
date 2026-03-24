using UnityEngine;

public partial class GameSetup
{
    /// <summary>Retorna o FPS alvo do jogo.</summary>
    /// <remarks>
    /// Esse valor é apenas uma intenção de frame rate, não garante que o jogo esteja realmente rodando nesse FPS.
    /// </remarks>
    public static int FramesPerSecond()
    {
        return Application.targetFrameRate > 0 ? Application.targetFrameRate : 60;
    }

    /// <summary>Retorna o tempo desejado por frame em milissegundos</summary>
    /// <remarks>
    /// Esse valor se baseia em uma intenção de frame rate, não garante que o jogo esteja realmente rodando nesse FPS.
    /// </remarks>
    public static long FrameBudgetInMs()
    {
        return 1000L / FramesPerSecond();
    }
}
