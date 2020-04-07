using System.Collections.Generic;
using UnityEngine;

public class ComponentPool<T> : IDisposable, IPool<T> where T : Component
{
    private bool isDisposed;
    private readonly uint expandBy;
    private readonly Stack<T> objects;
    private readonly List<T> created;
    private readonly T prefab;
    private readonly Transform parent;

    public ComponentPool(uint initSize, T prefab, uint expandBy, Transform parent)
    {
        this.expandBy = expandBy;
        this.prefab = prefab;
        this.parent = parent;
        prefab.gameObject.SetActive(false);
        objects = new Stack<T>();
        created = new List<T>();

        Expand((uint)Mathf.Max(1, initSize));
    }



    public T Rent(bool returnActive)
    {
        if (objects.Count == 0)
        {
            Expand(expandBy);
        }
        T instance = objects.Pop();
        return instance;
    }
    private void Expand(uint amount)
    {
        for (int i = 0; i < amount; i++)
        {
            T instance = Object.Instantiate(prefab, parent);
            instance.gameObject.AddComponent<EmitOnDisable>().OnDisableGameObject += UnRent;

            objects.Push(instance);
            created.Add(instance);
        }
    }
    private void UnRent(GameObject gameObject)
    {
        if (!isDisposed)
        {
            objects.Push(gameObject.GetComponent<T>());
        }
    }

    public void Dispose()
    {
        isDisposed = true;
        Clean();
    }

    public void Clean()
    {
        foreach (T component in objects)
        {
            if (component != null)
            {
                component.GetComponent<EmitOnDisable>().OnDisableGameObject -= UnRent;
                Object.Destroy(component.gameObject);
            }
        }
    }
}
