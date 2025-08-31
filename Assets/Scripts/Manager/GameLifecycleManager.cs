using System;
using VContainer;
using VContainer.Unity;

public class GameLifecycleManager : IStartable, IDisposable
{
    [Inject] private HomeScreen homeScreen;
    [Inject] private BarbecueParty barbequeParty;
    [Inject] private WalletController walletController;

    public void Start()
    {
        barbequeParty.Initialize();
        homeScreen.Initialize();
        UnityEngine.Debug.Log("Initializing WalletController");
        walletController.Initialize();
    }
    
    public void Dispose()
    {
    }
}
