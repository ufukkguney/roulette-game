using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private Image popupImage;
    [SerializeField] private TextMeshProUGUI popupText;
    [SerializeField] private Button closeButton;
    [SerializeField] private RectTransform target;
    private Vector3 startPopupImagePos;
    private bool isResetGame;
    public void Initialize()
    {
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        gameObject.SetActive(false);

        startPopupImagePos = popupImage.rectTransform.anchoredPosition;
    }
    public void Set(Sprite image, string text, bool hasUnSelectItem)
    {
        popupImage.sprite = image;

        if (!hasUnSelectItem)
        {
            popupText.text = "You are winner!";
            isResetGame = true;
        }
        else
            popupText.text = "You earned a " + text;


        gameObject.SetActive(true);
    }

    private void OnCloseButtonClicked()
    {
        popupImage.transform.DOMove(target.position, .75f).SetEase(Ease.InOutBack).OnStart(() =>
        {
            popupImage.rectTransform.DOScale(0.5f, .75f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                popupImage.rectTransform.localScale = Vector3.one;
            });
        }).OnComplete(() =>
        {
            popupImage.rectTransform.anchoredPosition = startPopupImagePos;
            gameObject.SetActive(false);

            if (isResetGame)
            {
                EventManager.InvokeBarbequeClose();
                isResetGame = false;
            }
        });
    }

    public void Dispose()
    {
        closeButton.onClick.RemoveListener(OnCloseButtonClicked);
    }
}

