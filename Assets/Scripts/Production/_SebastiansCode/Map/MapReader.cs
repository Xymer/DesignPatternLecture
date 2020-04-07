using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine.Assertions;
using TowerDefense;
using UnityEngine;

[System.Serializable]
public class MapKeyData
{
    private readonly TileType m_Type;
    private readonly GameObject m_Prefab;
    public TileType Type => m_Type;

    public GameObject Prefab => m_Prefab;
    public MapKeyData(TileType type, GameObject prefab)
    {
        m_Type = type;
        m_Prefab = prefab;
    }
}
public class MapReader
{
    private readonly Dictionary<TileType, GameObject> prefabsById;
    public MapReader(IEnumerable<MapKeyData> mapKeyData, string mapName)
    {
        m_MapName = mapName;
        prefabsById = new Dictionary<TileType, GameObject>();
        foreach (MapKeyData data in mapKeyData)
        {
            prefabsById.Add(data.Type, data.Prefab);
        }
        ReadString();
        SetupMapArray();
        SetupEnemyArray();
    }
    private const string BLOCK_SPLITTER = "#";
    private const char SPACE = ' ';
    [Header("Map Name")]
    private string m_MapName = "";

    private int[,] m_MapData;
    private int[,] m_EnemyArray;
    private string[] m_Data;
    private string[] m_EnemyData;
    private int totalWaves = 0;
    void ReadString()
    {
        int ySize = 0;
        int enemyYSize = 0;
        string filePath = "Assets/Resources/" + ProjectPaths.RESOURCES_MAP_SETTINGS + m_MapName + ".txt";
        bool startEnemyArray = false;
        using (StreamReader reader = new StreamReader(filePath))
        {
            List<string> tempMapData = new List<string>();
            List<string> tempEnemyData = new List<string>();

            bool reading = true;

            while (true)
            {
                if (!startEnemyArray && reading)
                {
                    string line = reader.ReadLine();

                    if (line != BLOCK_SPLITTER)
                    {
                        tempMapData.Add(line);
                        ySize++;
                    }
                    else
                    {
                        startEnemyArray = true;
                    }
                }
                else if (startEnemyArray && reading)
                {
                    string line = reader.ReadLine();

                    if (line == null)
                    {
                        reading = false;
                    }

                    else
                    {
                        tempEnemyData.Add(line);

                        enemyYSize++;
                        totalWaves = enemyYSize;
                    }
                }
                else
                {
                    m_Data = new string[ySize];
                    m_MapData = new int[tempMapData[0].Length, ySize];
                    m_EnemyData = new string[enemyYSize];
                    m_EnemyArray = new int[tempEnemyData[0].Split(SPACE).Length, enemyYSize];
                    for (int i = 0; i < tempMapData.Count; i++)
                    {
                        m_Data[i] = tempMapData[i];
                    }
                    for (int i = 0; i < tempEnemyData.Count; i++)
                    {
                        m_EnemyData[i] = tempEnemyData[i];
                    }
                    tempMapData.Clear();
                    tempEnemyData.Clear();
                    break;
                }
            }
        }
    }
    void SetupMapArray()
    {
        for (int y = 0; y < m_Data.Length; y++)
        {
            for (int x = 0; x < m_Data[y].Length; x++)
            {
                m_MapData[x, y] = int.Parse(m_Data[y][x].ToString());
            }
        }
    }
    void SetupEnemyArray()
    {
        for (int y = 0; y < m_EnemyData.Length; y++)
        {
            string[] currentWave = m_EnemyData[y].Split(SPACE);
            for (int x = 0; x < currentWave.Length; x++)
            {
                m_EnemyArray[x, y] = int.Parse(currentWave[x]);
            }
        }
    }

    public void SetMap(string mapName)
    {
        m_MapName = mapName;
        ReadString();
        SetupMapArray();
        SetupEnemyArray();
    }
    public int[,] GetMapData()
    {
        return m_MapData;
    }
    public int[,] GetEnemyData()
    {
        return m_EnemyArray;
    }
    public Dictionary<TileType, GameObject> GetDictionaryOfPrefabs()
    {
        return prefabsById;
    }
}
