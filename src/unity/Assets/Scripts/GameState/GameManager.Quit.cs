using Game.Core.Services;
using UnityEngine;

public partial class GameManager
{
    private static void SubscribeQuitEvent(IEvents events, IGameLogger logger)
    {
        events.Subscribe<QuitEvent>((evt) => DoQuit(evt, logger));
    }

    private static void DoQuit(QuitEvent evt, IGameLogger logger)
    {
        logger.Info?.Log("Quitting game!");
        // TODO: inform server that the player is quitting (simulating with 1 second delay)
        1
            .Seconds()
            .Delay()
            .ContinueWith(_ =>
            {
                logger.Info?.Log($"Quiting due to: {evt.Reason}");
#if UNITY_EDITOR
                // NOTE: telling the Unity editor to stop the player if running on editor
                UnityEditor.EditorApplication.delayCall += UnityEditor.EditorApplication.ExitPlaymode;
#endif
                Application.Quit(exitCode: 0);
            });
    }
}

/// <summary>Evento que indica que o jogo vai fechar.</summary>
public readonly struct QuitEvent
{
    /// <summary>
    /// Indica de onde veio esse evento: jogador clicou no botão de fechar? Deu um erro irrecuperável e é melhor fechar?
    /// </summary>
    public string Reason { get; }

    /// <inheritdoc cref="QuitEvent" />
    public QuitEvent(string reason)
    {
        Reason = reason;
    }
}
