using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class Popup : MonoBehaviour
{
    #region Fields
    [SerializeField] private Image popupImage;
    [SerializeField] private TextMeshProUGUI popupText;
    [SerializeField] private Button closeButton;
    [SerializeField] private RectTransform target;
    [SerializeField] private GameObject popupRoot;
    private Vector3 startPopupImagePos;
    private bool shouldResetGame;
    [Inject] private ItemFactory itemFactory;
    #endregion

    public void Initialize()
    {
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        popupRoot.SetActive(false);
        startPopupImagePos = popupImage.rectTransform.anchoredPosition;
    }

    public void Set(Sprite image, string text, bool hasUnSelectItem)
    {
        popupImage.sprite = image;
        popupText.text = hasUnSelectItem ? $"You earned a {text}" : "You are winner!";
        shouldResetGame = !hasUnSelectItem ? true : false;
        popupRoot.SetActive(true);
    }

    private void OnCloseButtonClicked()
    {
        AnimatePopup(() =>
        {
            PlayParticle();
            ResetPopup();
        });
    }

    private void AnimatePopup(System.Action onComplete)
    {
        popupImage.transform.DOMove(target.position, .75f)
            .SetEase(Ease.InOutBack)
            .OnStart(() =>
            {
                popupImage.rectTransform.DOScale(0.5f, .75f)
                    .SetEase(Ease.InOutSine)
                    .OnComplete(() => popupImage.rectTransform.localScale = Vector3.one);
            })
            .OnComplete(() => onComplete?.Invoke());
    }

    private void PlayParticle()
    {
        var particle = itemFactory.ParticlePool.Get(target);
        if (particle != null)
        {
            particle.Play();
            StartCoroutine(ReturnParticleAfterPlayCoroutine(particle));
            if (shouldResetGame)
            {
                EventManager.InvokeBarbequeClose();
                shouldResetGame = false;
            }
        }
    }

    private void ResetPopup()
    {
        popupImage.rectTransform.anchoredPosition = startPopupImagePos;
        popupRoot.SetActive(false);
    }

    private IEnumerator ReturnParticleAfterPlayCoroutine(ParticleSystem particle)
    {
        yield return new WaitWhile(() => particle.isPlaying);
        itemFactory.ParticlePool.Return(particle);
    }

    public void Dispose()
    {
        closeButton.onClick.RemoveListener(OnCloseButtonClicked);
    }
}

