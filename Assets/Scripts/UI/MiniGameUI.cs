using UnityEngine;
using UnityEngine.UI;

public class MiniGameUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button walletButton;
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
        playButton.interactable = false;
        EventManager.InvokeBarbequePlay();
    }

    private void OnWalletButtonClicked()
    {
        EventManager.InvokeWalletOpen();
    }
    
    private void OnItemEarned(ItemEarnedEventArgs args)
    {
        playButton.interactable = true;
        genericPopup.Set(args.itemSprite, args.itemType, args.hasUnSelectItem);
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
