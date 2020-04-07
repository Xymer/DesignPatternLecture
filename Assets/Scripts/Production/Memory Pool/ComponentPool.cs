using System.Collections.Generic;
using UnityEngine;

public class ComponentPool<T> : IDisposable, IPool<T> where T : Component
{
    private bool m_IsDisposed;
    private readonly uint m_ExpandBy;
    private readonly Stack<T> m_Objects;
    private readonly List<T> m_Created;
    private readonly T m_Prefab;
    private readonly Transform m_Parent;

    public ComponentPool(uint initSize, T prefab, uint expandBy, Transform parent)
    {
        m_ExpandBy = expandBy;
        m_Prefab = prefab;
        m_Parent = parent;
        prefab.gameObject.SetActive(false);
        m_Objects = new Stack<T>();
        m_Created = new List<T>();

        Expand((uint)Mathf.Max(1, initSize));
    }



    public T Rent(bool returnActive)
    {
        if (m_Objects.Count == 0)
        {
            Expand(m_ExpandBy);
        }
        T instance = m_Objects.Pop();
        return instance;
    }
    private void Expand(uint amount)
    {
        for (int i = 0; i < amount; i++)
        {
            T instance = Object.Instantiate(m_Prefab, m_Parent);
            instance.gameObject.AddComponent<EmitOnDisable>().OnDisableGameObject += UnRent;

            m_Objects.Push(instance);
            m_Created.Add(instance);
        }
    }
    private void UnRent(GameObject gameObject)
    {
        if (!m_IsDisposed)
        {
            m_Objects.Push(gameObject.GetComponent<T>());
        }
    }

    public void Dispose()
    {
        m_IsDisposed = true;
        Clean();
    }

    public void Clean()
    {
        foreach (T component in m_Objects)
        {
            if (component != null)
            {
                component.GetComponent<EmitOnDisable>().OnDisableGameObject -= UnRent;
                Object.Destroy(component.gameObject);
            }
        }
    }
}
