using UnityEngine;

public class ItemFactory
{
    public ObjectPool<CircleRow> CircleRowPool { get; private set; }
    public ObjectPool<CircleGlow> CircleGlowPool { get; private set; }
    public ObjectPool<WalletItem> WalletItemPool { get; private set; }
    public ObjectPool<ParticleSystem> ParticlePool { get; private set; }

    public void Initialize(Transform poolParent, int circleRowCount)
    {
        CircleRowPool = new ObjectPool<CircleRow>(Constants.CircleRowKey, poolParent);
        CircleGlowPool = new ObjectPool<CircleGlow>(Constants.CircleGlowKey, poolParent);
        WalletItemPool = new ObjectPool<WalletItem>(Constants.WalletItemKey, poolParent);
        ParticlePool = new ObjectPool<ParticleSystem>(Constants.ParticleKey, poolParent);

        CircleRowPool.Preload(circleRowCount);
        CircleGlowPool.Preload(Constants.CircleGlowPoolSize);
        WalletItemPool.Preload(Constants.WalletItemPoolSize);
        ParticlePool.Preload(Constants.ParticlePoolSize);
    }
}
