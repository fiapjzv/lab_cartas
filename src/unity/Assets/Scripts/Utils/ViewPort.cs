using UnityEngine;

public static class ViewPort
{
    public static Vector2 SizeInWorldUnits(Camera? cam = null)
    {
        cam ??= Camera.main;
        if (cam == null)
        {
            return Vector2.zero;
        }

        var height = cam.orthographicSize * 2f;
        var width = height * cam.aspect;

        return new Vector2(width, height);
    }
}
