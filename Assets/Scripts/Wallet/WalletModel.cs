using System;
using System.Collections.Generic;

[Serializable]
public class WalletModel
{
    private readonly Dictionary<ItemType, int> ownedItems = new();
    public IReadOnlyDictionary<ItemType, int> OwnedItems => ownedItems;

    public void AddItem(ItemType item, int count = 1)
    {
        if (ownedItems.ContainsKey(item))
            ownedItems[item] += count;
        else
            ownedItems[item] = count;
    }

    public bool RemoveItem(ItemType item, int count = 1)
    {
        if (!ownedItems.ContainsKey(item)) return false;
        if (ownedItems[item] < count) return false;
        ownedItems[item] -= count;
        if (ownedItems[item] <= 0)
            ownedItems.Remove(item);
        return true;
    }
}
