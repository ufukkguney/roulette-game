public static class Constants
{
#region PrefabPaths
    public const string CircleRowKey = "Assets/Prefabs/CircleRow.prefab";
    public const string CircleGlowKey = "Assets/Prefabs/CircleGlow.prefab";
    public const string WalletItemKey = "Assets/Prefabs/WalletItem.prefab";
#endregion

#region AnimationDurations
    public const float DefaultFadeDuration = 0.25f;
    public const float BlinkDuration = 0.1f;
    public const float SequenceDuration = 0.1f;
    public const float DeselectDelay = 1.0f;
#endregion

#region PoolSettings
    internal const int CircleGlowPoolSize = 6;
    internal const int WalletItemPoolSize = 9;
    public const int PoolInstantiateLimit = 3;
#endregion

#region GameLogic
    public const int BlinkAmount = 4;
    public const int SelectRepeatCCount = 30;
#endregion
}
