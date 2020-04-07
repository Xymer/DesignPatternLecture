using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{

    private static T m_instance;
    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                T[] instances = FindObjectsOfType<T>();
                if (instances.Length > 0)
                {
                    throw new System.InvalidCastException($"there is more than one {typeof(T).Name} instance in the scene");
                }
                if (instances.Length > 0)
                {
                    m_instance = instances[0];
                }
                if (m_instance == null)
                {
                    object[] customAttributes = typeof(T).GetCustomAttributes(typeof(SecureSingletonAttribute), false);
                    if (customAttributes.Length > 0 && customAttributes[0] is SecureSingletonAttribute attribute)
                    {
                        GameObject gameObject = new GameObject(typeof(T).Name);
                        m_instance = gameObject.AddComponent<T>();
                    }
                    else
                    {
                        object[] singletonConfigs = typeof(T).GetCustomAttributes(typeof(SingletonConfiguration), false);
                        if (singletonConfigs.Length > 0 && singletonConfigs[0] is SingletonConfiguration singletonConfig)
                        {
                            string path = singletonConfig.ResourcesPath;

                            GameObject singletonPrefab = Resources.Load<GameObject>(path);
                            if (singletonPrefab == null)
                            {
                                throw new System.NullReferenceException($"there is no {typeof(T).Name} prefab in the Resources folder");
                            }
                            GameObject gameObjectInstance = Instantiate(singletonPrefab);
                            m_instance = gameObjectInstance.GetComponent<T>();
                            if (m_instance == null)
                            {
                                throw new System.NullReferenceException($"There is no {typeof(T).Name} component attached to the singleton prefab");
                            }
                        }
                        else
                        {
                            throw new System.InvalidOperationException($"The singleton type {typeof(T).Name} doesn't include the mandatory attribute {typeof(SingletonConfiguration).Name}");
                        }
                       
                    }
                }
                DontDestroyOnLoad(m_instance.gameObject);
            }
            return m_instance;
        }
    }
    protected virtual void Awake()
    {
        if (m_instance == null)
        {
            m_instance = (T)this;
            DontDestroyOnLoad(gameObject);

        }
        else if (m_instance != this)
        {
            throw new System.InvalidOperationException($"There is more than one {typeof(T).Name} instance in the scene");
        }
    }
}
