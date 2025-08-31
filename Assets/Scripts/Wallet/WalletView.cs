using System;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class WalletView : MonoBehaviour, IDisposable
{
    [SerializeField] private Transform itemContainer;
    [SerializeField] private Button closeButton;
    [SerializeField] private Transform walletPanel;
    private WalletController walletController;

    public void Initialize(WalletController walletController)
    {
        this.walletController = walletController;
        closeButton.onClick.AddListener(CloseWalletPanel);

        EventManager.OnWalletOpen += OpenWalletPanel;
    }
    private void OpenWalletPanel()
    {
        walletController.SpawnWalletItems(itemContainer);
        walletPanel.gameObject.SetActive(true);
    }
    private void CloseWalletPanel()
    {
        walletController.ReturnAllWalletItemsToPool();
        walletPanel.gameObject.SetActive(false);
    }

    public void Dispose()
    {
        EventManager.OnWalletOpen -= OpenWalletPanel;
        closeButton.onClick.RemoveListener(CloseWalletPanel);
    }
}
