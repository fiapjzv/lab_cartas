using UnityEngine;

public partial class GameManager
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

    // TODO: unify the UI palette (on the uss) with the game palette using a helper method like:
    //       `root.style.SetProperty("--color-bg-main", Palette.BgMainColor);`
    /// <summary>Qual cor mostrar quando nada está sendo exibido na tela.</summary>
    public const string CLEAR_SCREEN_COLOR = "#121212";

    public static class DepthLayers
    {
        public const float CAMERA_GLOBAL_Z = -10f;
        public const float TABLE_ZONE_Z = 0f;
    }
}
