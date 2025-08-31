using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class WalletController
{
    [Inject] private readonly WalletModel walletModel;
    [Inject] private WalletView walletView;
    [Inject] private ItemManager itemManager;
    private readonly List<WalletItem> activeWalletItems = new();

    public void Initialize()
    {
        walletView.Initialize(this);
    }

    public void AddItem(ItemType item, int count = 1)
    {
        walletModel.AddItem(item, count);
    }
    public bool RemoveItem(ItemType item, int count = 1)
    {
        return walletModel.RemoveItem(item, count);
    }
    public void SpawnWalletItems(Transform parent)
    {
        foreach (var kvp in walletModel.OwnedItems)
        {
            var walletItem = itemManager.GetWalletItemUI(parent);
            if (walletItem != null)
            {
                Sprite sprite = itemManager.GetSpriteDependsOnType(kvp.Key);
                walletItem.SetWalletItem(sprite, kvp.Value);
                activeWalletItems.Add(walletItem);
            }
        }
    }

    public void ReturnAllWalletItemsToPool()
    {
        foreach (var walletItem in activeWalletItems)
        {
            itemManager.ReturnWalletItem(walletItem);
        }
        activeWalletItems.Clear();
    }
}
