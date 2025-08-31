using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class ItemManager
{
    [Inject] private ItemFactory itemFactory;
    [Inject] private ItemSprites rewardSprites;

    private Transform poolParent;
    private List<Transform> spawnPoints;
    private List<CircleRow> activeRows;

    public void InitializePool()
    {
        itemFactory.Initialize(poolParent, spawnPoints.Count);
    }

    public void SetParams(Transform poolParent, List<Transform> spawnPoints, List<CircleRow> activeRows)
    {
        this.poolParent = poolParent;
        this.spawnPoints = spawnPoints;
        this.activeRows = activeRows;
    }
    public void SpawnAllItems()
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

    public void ReturnAllItemsToPool()
    {
        foreach (var circleRow in activeRows)
        {
            itemFactory.CircleRowPool.Return(circleRow);
        }
        activeRows.Clear();
    }

    public CircleGlow GetCircleGlow(Transform target)
    {
        return itemFactory.CircleGlowPool.Get(target);
    }

    public void ReturnCircleGlow(CircleGlow glow)
    {
        itemFactory.CircleGlowPool.Return(glow);
    }

    public Sprite GetSpriteDependsOnType(ItemType itemType)
    {
        return rewardSprites.GetSpriteDependsOnType(itemType);
    }
    public WalletItem GetWalletItemUI(Transform parent)
    {
        return itemFactory.WalletItemPool.GetUI(parent);
    }

    public void ReturnWalletItem(WalletItem walletItem)
    {
        itemFactory.WalletItemPool.Return(walletItem);
    }

}
