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
    private readonly TileType type;
    private readonly GameObject prefab;
    public TileType Type => type;

    public GameObject Prefab => prefab;
    public MapKeyData(TileType type, GameObject prefab)
    {
        this.type = type;
        this.prefab = prefab;
    }
}
public class MapReader
{
    private readonly Dictionary<TileType, GameObject> prefabsById;
    public MapReader(IEnumerable<MapKeyData> mapKeyData,string mapName)
    {
        this.mapName = mapName;
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
    private string mapName = "";

    private int[,] mapData;
    private int[,] enemyArray;
    private string[] data;
    private string[] enemyData;
    private int totalWaves = 0;
    void ReadString()
    {
        int ySize = 0;
        int enemyYSize = 0;
        string filePath = "Assets/Resources/" + ProjectPaths.RESOURCES_MAP_SETTINGS + mapName + ".txt";
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
                    data = new string[ySize];
                    mapData = new int[tempMapData[0].Length, ySize];
                    enemyData = new string[enemyYSize];
                    enemyArray = new int[tempEnemyData[0].Split(SPACE).Length, enemyYSize];
                    for (int i = 0; i < tempMapData.Count; i++)
                    {
                        data[i] = tempMapData[i];
                    }
                    for (int i = 0; i < tempEnemyData.Count; i++)
                    {
                        enemyData[i] = tempEnemyData[i];
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
        for (int y = 0; y < data.Length; y++)
        {
            for (int x = 0; x < data[y].Length; x++)
            {
                mapData[x, y] = int.Parse(data[y][x].ToString());
            }
        }
    }
    void SetupEnemyArray()
    {
        for (int y = 0; y < enemyData.Length; y++)
        {
            string[] currentWave = enemyData[y].Split(SPACE);
            for (int x = 0; x < currentWave.Length; x++)
            {
                enemyArray[x, y] = int.Parse(currentWave[x]);
            }
        }
    }

    public void SetMap(string mapName)
    {
        this.mapName = mapName;
        ReadString();
        SetupMapArray();
        SetupEnemyArray();
    }
   public  int[,] GetMapData()
    {
        return mapData;
    }
    public int[,] GetEnemyData()
    {
        return enemyArray;
    }  
    public Dictionary<TileType, GameObject> GetDictionaryOfPrefabs()
    {
        return prefabsById;
    }
}
