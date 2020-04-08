using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObjectScriptablePool[] m_Enemies;
    [SerializeField] private MapReaderMono m_MapReader;
    [SerializeField] private Camera m_Camera;
    private PathAgent m_PathManager;
    private Vector3 m_StartPosition;
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
         
    }

    private void Initalize()
    {
        List<ScriptableEnemies> units = new List<ScriptableEnemies>();
        m_MapReader.GenerateMap();
        m_Camera.transform.position = new Vector3(m_MapReader.GetMapCenter().x, m_Camera.transform.position.y, m_MapReader.GetMapCenter().z);
        m_PathManager = new PathAgent(m_MapReader.GetMapGeneratorPath());
        List<Vector2Int> path = (List<Vector2Int>)m_PathManager.GetPath();
        m_StartPosition = new Vector3(path[0].x, 1, path[0].y);
    }
    [ContextMenu("ChangeMap")]
    private void ChangeMap()
    {
        m_MapReader.GenerateMap();
        m_PathManager.ChangePath(m_MapReader.GetMapGeneratorPath());
        m_Camera.transform.position = new Vector3(m_MapReader.GetMapCenter().x, m_Camera.transform.position.y, m_MapReader.GetMapCenter().z);

    }
    [ContextMenu("SpawnEnemy")]
    private void SpawnEnemy()
    {
        GameObject enemy = m_Enemies[0].Rent(true);
        enemy.transform.position = m_StartPosition;
        enemy.transform.rotation = Quaternion.identity;
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_MapReader != null && m_PathManager.GetPath() != null)
        {
            foreach (Vector2Int item in m_PathManager.GetPath())
            {
                Vector3 draw = new Vector3(item.x, 0, item.y);
                Gizmos.DrawSphere(draw, 1f);
            }
        }
    }

}
