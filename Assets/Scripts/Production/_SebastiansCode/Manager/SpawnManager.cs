using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnData
{
    [SerializeField] private ScriptableObject m_Enemy;
    [SerializeField] private int m_EnemyCount;

    public ScriptableObject Enemy => m_Enemy;
    public int EnemyCount => m_EnemyCount;
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private SpawnData[] m_SpawnDatas;
    [SerializeField] private MapReaderMono m_MapReader;

    private PathAgent m_PathManager;
  
    private void Awake()
    {
        m_MapReader = GetComponent<MapReaderMono>();
    }

    private void Start()
    {
        Initalize();
    }
    private void Update()
    {
        m_PathManager.MoveAgent();   
    }

    private void Initalize()
    {
        List<GameObject> enemies = new List<GameObject>();
        m_MapReader.GenerateMap();
        m_PathManager = new PathAgent(enemies ,m_MapReader.GetMapGeneratorPath());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_MapReader != null)
        {
            foreach (Vector2Int item in m_PathManager.GetPath())
            {
                Vector3 draw = new Vector3(item.x, 0, item.y);
                Gizmos.DrawSphere(draw, 1f);
            }
        }

    }
}
