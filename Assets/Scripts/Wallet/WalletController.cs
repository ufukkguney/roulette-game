using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using VContainer;

public class WalletController
{
    [Inject] private readonly WalletModel walletModel;
    [Inject] private WalletView walletView;
    [Inject] private ItemFactory itemFactory;
    [Inject] private ItemSprites itemSprites;
    private readonly List<WalletItem> activeWalletItems = new();

    // public WalletController(WalletModel model, ItemFactory factory, Transform parent, ItemSprites sprites)
    // {
    //     walletModel = model;
    //     itemFactory = factory;
    //     itemSprites = sprites;
    // }
    public void Initialize()
    {
        walletModel.Initialize();
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

    public int GetItemCount(ItemType item)
    {
        return walletModel.GetItemCount(item);
    }

    public bool HasItem(ItemType item)
    {
        return walletModel.HasItem(item);
    }

    public IReadOnlyDictionary<ItemType, int> GetAllItems()
    {
        return walletModel.OwnedItems;
    }

    public void SpawnWalletItems(Transform parent)
    {
        foreach (var kvp in walletModel.OwnedItems)
        {
            var walletItem = itemFactory.WalletItemPool.GetUI(parent);
            if (walletItem != null)
            {
                Sprite sprite = itemSprites.GetSpriteDependsOnType(kvp.Key);
                walletItem.SetWalletItem(sprite, kvp.Value);
                activeWalletItems.Add(walletItem);
            }
        }
    }

    public void ReturnAllWalletItemsToPool()
    {
        foreach (var walletItem in activeWalletItems)
        {
            Debug.Log($"Returning wallet item: {walletItem}");
            itemFactory.WalletItemPool.Return(walletItem);
        }
        activeWalletItems.Clear();
    }
}
