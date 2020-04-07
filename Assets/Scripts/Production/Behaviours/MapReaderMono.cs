using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapKeyDataMono
{
    [SerializeField] private TileType Type;
    [SerializeField] private GameObject Prefab;

    public TileType type { get => Type; }
    public GameObject prefab { get => Prefab; }
}
public class MapReaderMono : MonoBehaviour
{
    [SerializeField] private MapKeyDataMono[] mapKeyMonos;
    [SerializeField,Tooltip("Displacement of each tile when spawning")] private int tileDisplacement = 0;
    MapReader m_MapReader;
    MapGenerator m_MapGenerator;
    public string m_MapName = "map_1";
    private void Awake()
    {
        List<MapKeyData> data = new List<MapKeyData>();

        foreach (MapKeyDataMono keyDataMono in mapKeyMonos)
        {
            var d = new MapKeyData(keyDataMono.type, keyDataMono.prefab);
            data.Add(d);
        }
        m_MapReader = new MapReader(data,m_MapName);
        m_MapGenerator = new MapGenerator();      
    }
    [ContextMenu("Generate Map")]
    public void GenerateMap()
    {
        m_MapReader.SetMap(m_MapName);
        m_MapGenerator.GenerateMap(m_MapReader.GetMapData(), m_MapReader.GetDictionaryOfPrefabs(), tileDisplacement);
    }

    public IEnumerable<Vector2Int> GetMapGeneratorPath()
    {
        return m_MapGenerator.GetPath();
    }
}
