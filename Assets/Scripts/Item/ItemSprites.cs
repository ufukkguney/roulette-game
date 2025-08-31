using UnityEngine;

[CreateAssetMenu(fileName = "RewardSprites", menuName = "ScriptableObjects/RewardSprites")]
public class ItemSprites : ScriptableObject
{
    [System.Serializable]
    public struct ItemSpriteEntry
    {
        public ItemType rewardType;
        public Sprite sprite;
    }

    public ItemSpriteEntry[] rewardSprites;

    public Sprite GetRandomSpriteForReward()
    {
        var randomReward = GetRandomReward();
        return GetSpriteDependsOnType(randomReward);
    }

    public ItemType GetRandomReward()
    {
        var values = System.Enum.GetValues(typeof(ItemType));
        return (ItemType)values.GetValue(Random.Range(0, values.Length));
    }

    public Sprite GetSpriteDependsOnType(ItemType rewardType)
    {
        foreach (var entry in rewardSprites)
        {
            if (entry.rewardType == rewardType)
                return entry.sprite;
        }

        return null;
    }
}
