using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public abstract class MiniGameBase : MonoBehaviour, IDisposable
{
    [SerializeField] protected List<Transform> spawnPoints;
    protected List<CircleRow> activeRows = new();
    [Inject] protected ItemManager itemManager;
    [Inject] protected WalletController walletController;

    [SerializeField] protected Transform poolParent;
    [SerializeField] protected MiniGameUI miniGameUI;

    [Inject] protected GameSequenceManager sequenceManager;

    public virtual void Initialize()
    {
        itemManager.SetParams(poolParent, spawnPoints, activeRows);
        itemManager.InitializePool();
    }

    public virtual void MiniGameOpen()
    {
        itemManager.SpawnAllItems();
        sequenceManager.SetParams(spawnPoints, activeRows, walletController);
        transform.gameObject.SetActive(true);

        miniGameUI.Initialize();
    }

    public virtual void ResetMiniGame()
    {
        itemManager.ReturnAllItemsToPool();
        transform.gameObject.SetActive(false);
        sequenceManager.Reset();

        miniGameUI.Dispose();
    }

    public virtual void SelectAndPlaySequence(Action onSelectComplete = null)
    {
        StartCoroutine(sequenceManager.SelectAndPlaySequence(Constants.SequenceDuration, onSelectComplete));
    }

    public virtual void Dispose()
    {
        ResetMiniGame();
    }
}
