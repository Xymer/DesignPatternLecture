using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private const float HALF = 0.5f;

    List<GameObject> m_ObjectsPlaced = new List<GameObject>();
    IPathFinder m_PathFinder;
    Vector3 m_MapCenter = Vector3.zero;
    IEnumerable<Vector2Int> m_Path = new List<Vector2Int>();
    List<Vector2Int> m_AccessibleNodes = new List<Vector2Int>();

    public void GenerateMap(int[,] mapData, Dictionary<TileType, GameObject> prefabsById, int displacement)
    {
       
        Vector2Int walkableTile = Vector2Int.zero;
        if (m_ObjectsPlaced.Count != 0)
        {
            foreach (GameObject gameObject in m_ObjectsPlaced)
            {
                Destroy(gameObject);
            }
            m_AccessibleNodes.Clear();
        }

        int currentTile = 0;
        GameObject currentPrefab;

        TileType tileType;
        int yLength = mapData.GetLength(1);
        int xLength = mapData.GetLength(0);
        Vector2Int start = Vector2Int.zero;
        Vector2Int end = Vector2Int.zero;
        for (int y = 0; y < yLength; y++)
        {
            for (int x = 0; x < xLength; x++)
            {
                currentTile = mapData[x, y];
                tileType = TileMethods.TypeById[currentTile];
                currentPrefab = prefabsById[tileType];
                if (tileType == TileType.Start)
                {
                    start = new Vector2Int(x * displacement, y * displacement);
                    m_AccessibleNodes.Insert(0, start);
                }
                if (tileType == TileType.End)
                {
                    end = new Vector2Int(x * displacement, y * displacement);
                }
                if (tileType == TileType.Path)
                {
                    walkableTile = new Vector2Int(x * displacement, y * displacement);

                    if (!m_AccessibleNodes.Contains(walkableTile))
                    {
                        m_AccessibleNodes.Add(walkableTile);
                    }
                }
                m_ObjectsPlaced.Add(GameObject.Instantiate(currentPrefab, new Vector3Int(x * displacement, 0, y * displacement), Quaternion.identity));

                if (y == Mathf.CeilToInt(yLength * HALF) && x == Mathf.CeilToInt(xLength * HALF))
                {
                    m_MapCenter = new Vector3(x * displacement, 0,y * displacement);
                }
            }
        }

        m_AccessibleNodes.Add(end);
        m_PathFinder = new Dijkstra(m_AccessibleNodes);
        m_Path = m_PathFinder.FindPathWithDisplacement(start, end,displacement);
        
    }

    public Vector3 GetMapCenter()
    {
        return m_MapCenter;
    }
    public IPathFinder GetPathFinder()
    {
        return m_PathFinder;
    }
    public IEnumerable<Vector2Int> GetPath()
    {
        return m_Path;
    }


}
