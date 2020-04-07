using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnData
{
    [SerializeField] private ScriptableEnemies m_Enemy;
    [SerializeField] private int m_EnemyCount;

    public ScriptableObject Enemy => m_Enemy;
    public int EnemyCount => m_EnemyCount;
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Unit TempPrefab;
    [SerializeField] private MapReaderMono m_MapReader;
    [SerializeField] private Camera m_Camera;
    private PathAgent m_PathManager;
    private Vector3 startPosition;
    private void Awake()
    {
        m_MapReader = GetComponent<MapReaderMono>();
        m_Camera = Camera.main;
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
        List<Unit> units = new List<Unit>();
        m_MapReader.GenerateMap();       
        m_Camera.transform.position = new Vector3(m_MapReader.GetMapCenter().x,m_Camera.transform.position.y,m_MapReader.GetMapCenter().y);
        m_PathManager = new PathAgent(units ,m_MapReader.GetMapGeneratorPath());
        startPosition = new Vector3(m_PathManager.GetPath(0).x,1, m_PathManager.GetPath(0).y);
    }
    [ContextMenu("ChangeMap")]
    private void ChangeMap()
    {
        m_MapReader.GenerateMap();
        m_PathManager.ChangePath(m_MapReader.GetMapGeneratorPath());
        m_Camera.transform.position = new Vector3(m_MapReader.GetMapCenter().x, m_Camera.transform.position.y, m_MapReader.GetMapCenter().y);

    }
    [ContextMenu("SpawnEnemy")]
    private void SpawnEnemy()
    {        
        Instantiate(TempPrefab,startPosition,Quaternion.identity);
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
