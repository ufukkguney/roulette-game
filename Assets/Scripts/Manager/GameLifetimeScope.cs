using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    public ItemSprites rewardSprites;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(rewardSprites);
        builder.Register<ItemFactory>(Lifetime.Singleton);

        builder.Register<WalletModel>(Lifetime.Singleton);
        builder.Register<WalletController>(Lifetime.Singleton);
        builder.RegisterComponentInHierarchy<WalletView>();

        builder.RegisterComponentInHierarchy<BarbecueParty>();
        builder.RegisterComponentInHierarchy<HomeScreen>();

        builder.RegisterEntryPoint<GameLifecycleManager>();

    }
}
