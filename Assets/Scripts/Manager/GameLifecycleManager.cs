using VContainer;
using VContainer.Unity;

public class GameLifecycleManager : IStartable

{
    [Inject] private HomeScreen homeScreen;
    [Inject] private BarbecueParty barbequeParty;
    [Inject] private WalletController walletController;

    public void Start()
    {
        homeScreen.Initialize();
        barbequeParty.Initialize();
        walletController.Initialize();
    }
}
