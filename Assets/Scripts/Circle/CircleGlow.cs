using UnityEngine;
using DG.Tweening;
using System;
using System.Collections;

public class CircleGlow : MonoBehaviour, IPoolable
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Select(Transform target, Action onFadeOutComplete = null, float duration = Constants.DefaultFadeDuration)
    {
        transform.position = target.position;
        transform.localScale = target.localScale;
        spriteRenderer.DOFade(1f, duration)
            .OnComplete(() => spriteRenderer.DOFade(0f, duration)
                .OnComplete(() => onFadeOutComplete?.Invoke()));
    }
    public void MakeBlink(int counter, Transform target = null, Action onFadeOutComplete = null)
    {
        StartCoroutine(MakeBlinkWithSelect(counter, target, onFadeOutComplete));
    }
    private IEnumerator MakeBlinkWithSelect(int counter, Transform target, Action onFadeOutComplete = null)
    {
        for (int i = 0; i < counter; i++)
        {
            Select(target, null, Constants.BlinkDuration);
            yield return new WaitForSeconds(Constants.BlinkDuration * 2);
        }

        onFadeOutComplete?.Invoke();
    }
    
    public void ResetPoolObject()
    {
        gameObject.SetActive(false);
        spriteRenderer.DOFade(0f, 0f);
    }

}
