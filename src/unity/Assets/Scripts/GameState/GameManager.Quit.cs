using Game.Core.Services;
using UnityEngine;

public partial class GameManager
{
    private void SubscribeQuitEvent(IEvents events, IGameLogger logger)
    {
        events.Subscribe<QuitEvent>((evt) => DoQuit(evt, logger));
    }

    private void DoQuit(QuitEvent evt, IGameLogger logger)
    {
        logger.Info?.Log("Quitting game!");
        // TODO: inform server that the player is quitting (simulating with 1 second delay)
        1.Seconds().Delay().ContinueWith((_) => Application.Quit(exitCode: 0));
    }
}

public readonly struct QuitEvent
{
    public string Reason { get; }
}
