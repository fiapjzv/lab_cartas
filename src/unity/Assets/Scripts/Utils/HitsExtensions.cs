using Game.Core.Services;
using UnityEngine;

public static class HitsExtensions
{
    /// <summary>
    ///  Selects the topmost object by prioritizing the lowest Z-depth (closest to camera).<br/>
    /// X-axis as a tie-breaker (so we need to sort the render order based on this logic).
    /// </summary>
    public static Collider2D? GetHitOnTop<T>(this Collider2D[] hits)
    {
        // NOTE: Using positive infinity because the "Top" means closest to Camera
        //       and the default camera is on Z: -10 (see: GameManager.DepthLayers.CAMERA_GLOBAL_Z)
        var closestZ = float.MaxValue;
        var maxX = float.MinValue;

        Collider2D? topHit = null;
        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent<T>(out _))
            {
                continue;
            }

            var (currX, _, currZ) = hit.transform.position;

            if (currZ > closestZ)
            {
                continue;
            }

            if (Mathf.Approximately(currZ, closestZ) && currX < maxX)
            {
                continue;
            }

            closestZ = currZ;
            maxX = currX;
            topHit = hit;
        }

        return topHit;
    }
}
