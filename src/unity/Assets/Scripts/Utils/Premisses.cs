using System;
using Game.Core.Utils;
using UnityEngine;
using static GameManager;

// PERF: maybe only check this on debug?
public static class Premisses
{
    public static void ValidateCamera(Camera? cam = null)
    {
        cam ??= Camera.main ?? throw new ArgumentNullException("No Camera.main available!");

        Guard.Assert(() =>
            cam is not null && cam.orthographic ? Result.Ok() : Result.Err("We expect an orthographic camera!")
        );

        Guard.Assert(() =>
        {
            // NOTE: orthographicSize is HALF the total height
            var currentHeight = cam.orthographicSize * 2;
            return Mathf.Approximately(currentHeight, VIEWPORT_HEIGHT)
                ? Result.Ok()
                : Result.Err($"Camera height is {currentHeight}, but CardView expects {VIEWPORT_HEIGHT}!");
        });
    }
}
