// <summary>Cena foi completamente carregada.</summary>
public readonly struct SceneLoadedEvt
{
    /// <inheritdoc cref="Scene" />
    public Scene Scene { get; }

    /// <inheritdoc cref="SceneLoadedEvt" />
    public SceneLoadedEvt(Scene scene)
    {
        Scene = scene;
    }
}

// <summary>Cena está sendo carregada.</summary>
public readonly struct SceneLoadingEvt
{
    /// <inheritdoc cref="Scene" />
    public Scene Scene { get; }

    /// <summary>Progresso atual do carregamento (0.0 a 1.0).</summary>
    public float Progress { get; }

    /// <inheritdoc cref="SceneLoadingEvt" />
    public SceneLoadingEvt(Scene scene, float progress)
    {
        Scene = scene;
        Progress = progress;
    }
}
