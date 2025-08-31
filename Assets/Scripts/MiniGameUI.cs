using UnityEngine;
using UnityEngine.UI;

public class MiniGameUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button playButton;
    public Button walletButton;
    [SerializeField] private Popup genericPopup;

    public void Initialize()
    {
        genericPopup.Initialize();
        
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        playButton.onClick.AddListener(OnPlayButtonClicked);
        walletButton.onClick.AddListener(OnWalletButtonClicked);

    EventManager.OnItemEarned += OnItemEarned;
    }

    private void OnCloseButtonClicked()
    {
        EventManager.InvokeBarbequeClose();
    }

    private void OnPlayButtonClicked()
    {
        EventManager.InvokeBarbequePlay();
    }

    private void OnWalletButtonClicked()
    {
        Debug.Log("Wallet Button Clicked");
        EventManager.InvokeWalletOpen();
    }
    private void OnItemEarned(ItemEarnedEventArgs args)
    {
        genericPopup.Set(args.itemSprite, args.itemType, args.target, args.hasUnSelectItem);
    }
    public void Dispose()
    {
        closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        playButton.onClick.RemoveListener(OnPlayButtonClicked);
        walletButton.onClick.RemoveListener(OnWalletButtonClicked);

        EventManager.OnItemEarned -= OnItemEarned;
        genericPopup.Dispose();
    }

    
}
