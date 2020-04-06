using System.Collections.Generic;
using UnityEngine;

    public interface IPool<T>
    {
        T Rent(bool returnActive);
    }

    public class GameObjectPool : IPool<GameObject>
    {       
        private readonly uint expandBy;
        private readonly GameObject prefab;
        private readonly Transform parent;

        readonly Stack<GameObject> objects = new Stack<GameObject>();
        public GameObjectPool(uint initSize, GameObject prefab, uint expandBy = 1, Transform parent = null)
        {
           
            this.expandBy = (uint)Mathf.Max(1,expandBy);
            this.prefab = prefab;
            this.parent= parent;
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
            }
        }
        private void UnRent(GameObject gameObject)
        {
            objects.Push(gameObject);
        }
        public GameObject Rent(bool returnActive)
        {
            if (objects.Count == 0)
            {
                Expand(expandBy);
            }
  
            GameObject instance = objects.Pop();
            instance.SetActive(returnActive);

            return instance;
        }
    }
