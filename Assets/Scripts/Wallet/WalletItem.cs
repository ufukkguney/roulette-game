using TMPro;
using UnityEngine;
using UnityEngine.UI;

// IPoolable interface eklenmeli
using System;

public class WalletItem : MonoBehaviour, IPoolable
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI amountText;

    public void ResetPoolObject()
    {
        // Reset logic for WalletItem
        // Görsel, miktar, seçili durumu vs. sıfırla
        gameObject.SetActive(false);
        SetItemSprite(null);
        SetAmount(0);
    }

    public void SetItemSprite(Sprite sprite)
    {
        if (itemImage != null)
            itemImage.sprite = sprite;
    }

    public void SetAmount(int amount)
    {
        if (amountText != null)
            amountText.text = amount.ToString();
    }

    public void SetWalletItem(Sprite sprite, int amount)
    {
        SetItemSprite(sprite);
        SetAmount(amount);
    }
}
