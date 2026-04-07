using UnityEngine;

// <summary>Engatilha uma mudança de cena.</summary>
public readonly struct ChangeSceneEvt
{
    // <summary>Pra qual cena (<see crfe="Scene"/> )desejo mudar.</summary>
    public Scene Scene { get; }

    /// <inheritdoc cref="ChangeSceneEvt" />
    public ChangeSceneEvt(Scene scene)
    {
        Scene = scene;
    }
}

// <summary>Cena começou a ser carregada.</summary>
public readonly struct SceneLoadStartEvt
{
    /// <inheritdoc cref="Scene" />
    public Scene Scene { get; }

    /// <inheritdoc cref="SceneLoadStartEvt" />
    public SceneLoadStartEvt(Scene scene)
    {
        Scene = scene;
    }
}

// <summary>Cena está sendo carregada.</summary>
public readonly struct SceneLoadProgressEvt
{
    /// <inheritdoc cref="Scene" />
    public Scene Scene { get; }

    /// <summary>Progresso atual do carregamento (0.0 a 1.0).</summary>
    public float Progress { get; }

    /// <inheritdoc cref="SceneLoadProgressEvt" />
    public SceneLoadProgressEvt(Scene scene, float progress)
    {
        Scene = scene;
        Progress = progress;
    }
}

// <summary>Cena foi completamente carregada.</summary>
public class SceneLoadCompleteEvt
{
    /// <inheritdoc cref="Scene" />
    public Scene Scene { get; }

    // <summary>The Unity Scene that was loaded but is hidden.</summary>
    public AsyncOperation SceneLoaded { get; }

    /// <inheritdoc cref="SceneLoadCompleteEvt" />
    public SceneLoadCompleteEvt(Scene scene, AsyncOperation sceneLoaded)
    {
        Scene = scene;
        SceneLoaded = sceneLoaded;
    }
}
