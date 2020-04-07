using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : IPool<GameObject>, IDisposable
{
    private bool isDisposed;
    private readonly uint expandBy;
    private readonly GameObject prefab;
    private readonly Transform parent;

    private readonly Stack<GameObject> objects = new Stack<GameObject>();
    private readonly List<GameObject> created = new List<GameObject>();
    public GameObjectPool(uint initSize, GameObject prefab, uint expandBy = 1, Transform parent = null)
    {

        this.expandBy = (uint)Mathf.Max(1, expandBy);
        this.prefab = prefab;
        this.parent = parent;
        prefab.SetActive(false);
        Expand((uint)Mathf.Max(1, initSize));
    }
    private void Expand(uint amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject instance = GameObject.Instantiate(prefab, parent);
            EmitOnDisable emitOnDisable = instance.AddComponent<EmitOnDisable>();
            emitOnDisable.OnDisableGameObject += UnRent;

            objects.Push(instance);
            created.Add(instance);
        }
    }
    private void UnRent(GameObject gameObject)
    {
        if (isDisposed == false)
        {
            objects.Push(gameObject);
        }
    }
    public GameObject Rent(bool returnActive)
    {
        if (isDisposed)
        {
            return null;
        }
        if (objects.Count == 0)
        {
            Expand(expandBy);
        }

        GameObject instance = objects.Pop();
        instance.SetActive(returnActive);

        return instance;
    }
    public void Clear()
    {
        foreach (GameObject gameObject in created)
        {
            if (gameObject != null)
            {
                gameObject.GetComponent<EmitOnDisable>().OnDisableGameObject -= UnRent;
                Object.Destroy(gameObject);
            }
        }
        objects.Clear();
        created.Clear();
    }

    public void Dispose()
    {
        isDisposed = true;
        Clear();
    }
}
