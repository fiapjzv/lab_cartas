using UnityEngine;

/// <summary>Extensões simples para qualidade de vida (escrever menos).</summary>
public static class HelperExtensions
{
    /// <summary>Verifica se o GameObject possui um componente do tipo informado.</summary>
    public static bool HasComponent<T>(this Component component)
    {
        return component.GetComponent<T>() != null;
    }

    /// <inheritdoc cref="HasComponent{T}(Component)" />
    public static bool HasComponent<T>(this GameObject obj)
    {
        return obj.GetComponent<T>() != null;
    }
}
