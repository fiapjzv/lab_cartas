using System;
using Game.Core.Services;
using UnityEngine.UIElements;

/// <summary>
/// Centraliza verificações que garantem premissas.  O objetivo é facilitar o debug. Por exemplo:
/// <ul>
/// <li>Garantir que um elemento existe na UI e não mudou de nome.</lu>
/// <li>Garantir que um componente está linkado no Unity Editor.</lu>
/// </ul>
/// </summary>
public static class Guard
{
    /// <summary>
    /// Garante que o objeto não seja nulo.<br/>
    /// Caso seja nulo, registra o erro no logger e explode.
    /// </summary>
    /// <remarks>Programação defensiva contra o erro de um bilhão de dólares.</remarks>
    public static T NotNull<T>(T? obj, IGameLogger logger)
        where T : class
    {
        if (obj is not null)
        {
            return obj;
        }

        var error =
            $"Expected {typeof(T).Name} not to be null. You might have forgotten to link the component in Unity.";
        logger.Error?.Log(error);
        throw new InvalidOperationException(error);
    }

    /// <summary>
    /// Garante que um elemento existe dentro da hierarquia de UI Toolkit.
    /// Realiza a busca pelo nome e valida se o elemento foi encontrado.
    /// </summary>
    public static VisualElement ElementIsPresent<T>(
        VisualElement root,
        string elementName,
        IGameLogger logger
    )
        where T : VisualElement
    {
        var element = root.Q<T>(elementName);

        if (element is not null)
        {
            return element;
        }

        var error = $"Could not find element '{elementName}' on '{root.name}'";
        logger.Error?.Log(error);
        throw new InvalidOperationException(error);
    }

    /// <inheritdoc cref="ElementIsPresent{T}(VisualElement, string, IGameLogger)" />
    public static VisualElement ElementIsPresent(
        VisualElement root,
        string elementName,
        IGameLogger logger
    )
    {
        return ElementIsPresent<VisualElement>(root, elementName, logger);
    }
}
