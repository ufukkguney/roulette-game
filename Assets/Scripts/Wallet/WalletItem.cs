using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WalletItem : MonoBehaviour, IPoolable
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI amountText;

    public void ResetPoolObject()
    {
        gameObject.SetActive(false);
        SetItemSprite(null);
        SetAmount(0);
    }

    public void SetItemSprite(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }

    public void SetAmount(int amount)
    {
        amountText.text = amount.ToString();
    }

    public void SetWalletItem(Sprite sprite, int amount)
    {
        SetItemSprite(sprite);
        SetAmount(amount);
    }
}
