using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnData
{
    [SerializeField] private GameObject m_Prefab;
    [SerializeField] private int m_EnemyCount;

    public GameObject Prefab => m_Prefab;
    public int EnemyCount => m_EnemyCount;
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private SpawnData[] m_SpawnDatas;
    [SerializeField] private MapReaderMono m_MapReader;

    private void Awake()
    {
        m_MapReader = GetComponent<MapReaderMono>();
    }

    private void Start()
    {
        Initalize();
    }

    private void Initalize()
    {
        m_MapReader.GenerateMap();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_MapReader != null)
        {
            foreach (Vector2Int item in m_MapReader.GetMapGeneratorPath())
            {
                Vector3 draw = new Vector3(item.x, 0, item.y);
                Gizmos.DrawSphere(draw, 1f);
            }
        }

    }
}
