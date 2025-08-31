using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ObjectPool<T> where T : Component
{
    private readonly Queue<T> pool = new();
    private readonly string addressableKey;
    private readonly Transform parent;

    public ObjectPool(string key, Transform parent)
    {
        addressableKey = key;
        this.parent = parent;
    }

    public void Preload(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Addressables.InstantiateAsync(addressableKey, parent).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    var comp = handle.Result.GetComponent<T>();
                    if (comp != null)
                    {
                        comp.gameObject.SetActive(false);
                        pool.Enqueue(comp);
                    }
                }
            };
        }
    }

    private T GetInternal(System.Action<T> setupAction, Transform targetOrParent)
    {
        if (pool.Count < Constants.PoolInstantiateLimit)
        {
            Addressables.InstantiateAsync(addressableKey, parent).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    var newComp = handle.Result.GetComponent<T>();
                    if (newComp != null)
                    {
                        newComp.gameObject.SetActive(false);
                        pool.Enqueue(newComp);
                    }
                }
            };
        }

        if (pool.Count > 0)
        {
            var comp = pool.Dequeue();
            setupAction(comp);
            return comp;
        }
        else
        {
            return null;
        }
    }

    public T Get(Transform target)
    {
        return GetInternal(comp => SetupInstance(comp, target), target);
    }

    public T GetUI(Transform parentTransform)
    {
        return GetInternal(comp => SetupUIInstance(comp, parentTransform), parentTransform);
    }

    private void SetupInstance(T comp, Transform target)
    {
        var go = comp.gameObject;
        go.transform.position = target.position;
        go.transform.localScale = target.localScale;
        go.SetActive(true);
    }

    private void SetupUIInstance(T comp, Transform parentTransform)
    {
        var go = comp.gameObject;
        go.transform.SetParent(parentTransform, false);
        go.SetActive(true);
    }

    public void Return(T comp)
    {
        // comp.transform.SetParent(parent);
        if (comp is IPoolable poolable)
        {
            poolable.ResetPoolObject();
        }
        else
        {
            comp.gameObject.SetActive(false);
        }
        pool.Enqueue(comp);
    }
}
