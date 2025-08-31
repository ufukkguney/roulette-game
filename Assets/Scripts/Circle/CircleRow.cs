using System.Collections;
using UnityEngine;

public class CircleRow : MonoBehaviour, IPoolable
{
    [SerializeField] private SpriteRenderer ItemSpriteRenderer;
    [SerializeField] private SpriteRenderer TickSpriteRenderer;
    [SerializeField] private SpriteRenderer CircleRowRenderer;

    [Header("Sprite References")]
    [SerializeField] private Sprite blue;
    [SerializeField] private Sprite gray;
    [SerializeField] private Sprite baseSprite;

    public ItemType ItemType { get; private set; }

    public bool IsSelected { get; private set; }

    public void SetCircle(Sprite sprite, ItemType itemType)
    {
        ItemSpriteRenderer.sprite = sprite;
        ItemType = itemType;
    }

    public void SelectRow()
    {
        IsSelected = true;
        StartCoroutine(SelectRowCoroutine());
    }

    private IEnumerator SelectRowCoroutine()
    {
        CircleRowRenderer.sprite = blue;
        TickSpriteRenderer.gameObject.SetActive(true);
        yield return new WaitForSeconds(Constants.DeselectDelay);
        CircleRowRenderer.sprite = gray;
        ItemSpriteRenderer.sprite = null;
    }

    public void ResetPoolObject()
    {
        IsSelected = false;
        TickSpriteRenderer.gameObject.SetActive(false);
        CircleRowRenderer.sprite = baseSprite;
    }

}
