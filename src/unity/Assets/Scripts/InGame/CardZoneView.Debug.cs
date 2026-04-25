public partial class CardZoneView
{
#if UNITY_EDITOR || DEBUG
    private void OnDrawGizmos()
    {
        if (_boxCollider == null)
        {
            return;
        }

        var color = GetZoneColor(ZoneType);

        var center = transform.position + (UnityEngine.Vector3)_boxCollider.offset;
        var size = _boxCollider.size;

        UnityEngine.Gizmos.DrawWireCube(center, size);
        UnityEngine.Gizmos.color = new UnityEngine.Color(color.r, color.g, color.b, 0.2f);
    }

    private static UnityEngine.Color GetZoneColor(Type type)
    {
        return type switch
        {
            Type.PlayerHand => UnityEngine.Color.green,
            Type.PlayerDeck => UnityEngine.Color.blue,
            Type.InGameArea => UnityEngine.Color.yellow,
            Type.Graveyard => UnityEngine.Color.red,
            _ => throw new System.ArgumentOutOfRangeException(nameof(type), type.ToString()),
        };
    }
#endif
}
