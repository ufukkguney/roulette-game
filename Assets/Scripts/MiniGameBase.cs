using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public abstract class MiniGameBase : MonoBehaviour, IDisposable
{
    [SerializeField] protected List<Transform> spawnPoints;
    protected List<CircleRow> activeRows = new();
    [Inject] protected ItemFactory itemFactory;
    [Inject] protected ItemSprites rewardSprites;
    [Inject] protected WalletController walletController;

    [SerializeField] protected Transform poolParent;
    [SerializeField] protected MiniGameUI miniGameUI;

    public virtual void Initialize()
    {
        itemFactory.Initialize(poolParent, spawnPoints.Count);
        miniGameUI.Initialize();
    }

    public virtual void MiniGameOpen()
    {
        SpawnAllItems();
        transform.gameObject.SetActive(true);
    }

    public virtual void ResetMiniGame()
    {
        ReturnAllItemsToPool();
        transform.gameObject.SetActive(false);
    }

    protected virtual void SpawnAllItems()
    {
        if (spawnPoints.Count == 0 || itemFactory == null) return;
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            var circleRow = itemFactory.CircleRowPool.Get(spawnPoints[i]);
            ItemType itemType = rewardSprites.GetRandomReward();
            Sprite sprite = rewardSprites.GetSpriteDependsOnType(itemType);
            if (circleRow != null)
            {
                circleRow.SetCircle(sprite, itemType);
                activeRows.Add(circleRow);
            }
        }
    }
    protected virtual void ReturnAllItemsToPool()
    {
        foreach (var circleRow in activeRows)
        {
            itemFactory.CircleRowPool.Return(circleRow);
        }
        activeRows.Clear();
    }

    public virtual void PlayGlowSequence()
    {
        StopAllCoroutines();
        StartCoroutine(GlowSequenceCoroutine(Constants.SequenceDuration));
    }

    protected virtual IEnumerator GlowSequenceCoroutine(float delay)
    {
        int spawnCount = spawnPoints.Count;
        int repeatCount = UnityEngine.Random.Range(0, Constants.SelectRepeatCCount);

        // Satır seçimini fonksiyonlaştır
        int selectedRowIndex = repeatCount % spawnCount;
        int originalRepeatCount = repeatCount;
        int safety = 0;
        CircleRow selectedRow = activeRows[selectedRowIndex];
        while (selectedRow.IsSelected && safety < 100)
        {
            selectedRowIndex = UnityEngine.Random.Range(0, spawnCount);
            selectedRow = activeRows[selectedRowIndex];
            repeatCount = selectedRowIndex; // repeatCount'u güncelle
            safety++;
        }

        Debug.Log($"Selected Row Index: {selectedRowIndex}, Safety Count: {safety}");
        walletController.AddItem(selectedRow.ItemType);

        for (int i = 0; i <= repeatCount; i++)
        {
            int index = i % spawnCount;
            var glow = itemFactory.CircleGlowPool.Get(spawnPoints[index]);
            if (glow == null)
                continue;

            Action onGlowComplete = () => itemFactory.CircleGlowPool.Return(glow);

            if (i == repeatCount)
            {
                glow.MakeBlink(Constants.BlinkAmount, spawnPoints[index], () => {
                    selectedRow.SelectRow();
                    var args = new ItemEarnedEventArgs(
                        rewardSprites.GetSpriteDependsOnType(selectedRow.ItemType),
                        selectedRow.ItemType.ToString(),
                        miniGameUI.walletButton.transform as RectTransform,
                        HasUnselectedItem()
                    );
                    EventManager.InvokeItemEarned(args);
                    onGlowComplete();
                });
            }
            else
            {
                glow.Select(spawnPoints[index], onGlowComplete);
            }
            yield return new WaitForSeconds(delay);
        }
    }

    protected bool HasUnselectedItem()
    {
        foreach (var row in activeRows)
        {
            if (!row.IsSelected)
                return true;
        }
        return false;
    }

    public virtual void Dispose()
    {
        miniGameUI.Dispose();
    }
}
