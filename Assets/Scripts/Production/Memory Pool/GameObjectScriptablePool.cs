using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Pool/GameObject")]
public class GameObjectScriptablePool : ScriptableObject, IPool<GameObject>
{
    [SerializeField] private GameObject m_Prefab;
    [SerializeField] private uint m_InitSize;
    [SerializeField] private uint m_ExpandBy;
    [SerializeField] private bool m_HasParent;
    [SerializeField] private string m_ParentName;

    private GameObjectPool internalPool;


    public GameObject Rent(bool returnActive)
    {
        if (internalPool == null)
        {
            Transform parent = null;
            if (m_HasParent)
            {
                parent = new GameObject(m_ParentName).transform;
            }

            internalPool = new GameObjectPool(m_InitSize, m_Prefab, m_ExpandBy, parent);

        }
        return internalPool.Rent(returnActive);
    }
    public void OnDestroy()
    {
        internalPool.Dispose();
    }
}

