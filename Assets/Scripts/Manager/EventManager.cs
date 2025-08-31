using System;
using UnityEngine;

public static class EventManager
{
    public static event Action OnBarbequeOpen;
    public static event Action OnBarbequePlay;
    public static event Action OnBarbequeClose;
    public static event Action OnWalletOpen;
    public static event Action<ItemEarnedEventArgs> OnItemEarned;

    public static void InvokeBarbequeOpen()
    {
        OnBarbequeOpen?.Invoke();
    }
    public static void InvokeBarbequeClose()
    {
        OnBarbequeClose?.Invoke();
    }
    public static void InvokeBarbequePlay()
    {
        OnBarbequePlay?.Invoke();
    }
    public static void InvokeWalletOpen()
    {
        OnWalletOpen?.Invoke();
    }
    public static void InvokeItemEarned(ItemEarnedEventArgs args)
    {
        OnItemEarned?.Invoke(args);
    }
}

public struct ItemEarnedEventArgs
{
    public Sprite itemSprite;
    public string itemType;
    public RectTransform target;
    public bool hasUnSelectItem;

    public ItemEarnedEventArgs(Sprite itemSprite, string itemType, RectTransform target, bool hasUnSelectItem)
    {
        this.itemSprite = itemSprite;
        this.itemType = itemType;
        this.target = target;
        this.hasUnSelectItem = hasUnSelectItem;
    }
}
