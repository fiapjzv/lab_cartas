using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Services;
using UnityEngine;

/// <summary>Classe base para componentes. Integra logging, eventos e unsub automático.</summary>
public abstract class GameBehavior : MonoBehaviour
{
    protected IGameLogger Logger = null!;
    protected IEvents Events = null!;

    private readonly List<IDisposable> _subs = new();

    public void Awake()
    {
        Logger = Service.Get<IGameLogger>();
        Events = Service.Get<IEvents>();

        Init();
    }

    public void OnEnable()
    {
        _subs.AddRange(SubscribeEvents());
        WhenEnabled();
    }

    public void OnDisable()
    {
        EnsureUnsub();
        WhenDisabled();
    }

    public void OnDestroy()
    {
        EnsureUnsub();
        WhenDestroyed();
    }

    /// <summary>Ponto de entrada para inicialização de referências e lógica customizada de Awake.</summary>
    protected virtual void Init() { }

    /// <summary>Retorne as subscrições de evento que deseja fazer no OnEnable do componente.</summary>
    protected virtual IEnumerable<IDisposable> SubscribeEvents() => Enumerable.Empty<IDisposable>();

    /// <summary>Lógica customizada que roda depois de feitas as subscriptions.</summary>
    protected virtual void WhenEnabled() { }

    /// <summary>Lógica customizada que roda depois de cancelar as subscriptions.</summary>
    protected virtual void WhenDisabled() { }

    /// <summary>Lógica customizada que roda depois de cancelar as subscriptions.</summary>
    protected virtual void WhenDestroyed() { }

    private void EnsureUnsub()
    {
        if (_subs.Count == 0)
        {
            return;
        }

        Logger.Debug?.Log($"Unsubscribing {_subs.Count} game events");
        foreach (var sub in _subs)
        {
            sub.Dispose();
        }
        _subs.Clear();
    }
}
