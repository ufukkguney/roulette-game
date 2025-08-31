using System.Collections.Generic;

public class WalletModel
{
    private readonly Dictionary<ItemType, int> ownedItems = new();
    public IReadOnlyDictionary<ItemType, int> OwnedItems => ownedItems;

    public void Initialize()
    {
        // InitializeMockData();
    }

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

    public int GetItemCount(ItemType item)
    {
        return ownedItems.TryGetValue(item, out int count) ? count : 0;
    }

    public bool HasItem(ItemType item)
    {
        return ownedItems.ContainsKey(item);
    }

    public void InitializeMockData()
    {
        AddItem(ItemType.Candy, 5);
        AddItem(ItemType.Beer, 2);
        AddItem(ItemType.Pineapple, 7);
        AddItem(ItemType.Donut, 1);
    }
}
