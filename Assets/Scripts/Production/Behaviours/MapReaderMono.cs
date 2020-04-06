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
    MapReader mapReader;
    MapGenerator mapGenerator;
    public string mapName = "map_1";
    private void Awake()
    {
        List<MapKeyData> data = new List<MapKeyData>();

        foreach (MapKeyDataMono keyDataMono in mapKeyMonos)
        {
            var d = new MapKeyData(keyDataMono.type, keyDataMono.prefab);
            data.Add(d);
        }
        mapReader = new MapReader(data,mapName);
        mapGenerator = new MapGenerator();
       
    }
    [ContextMenu("Generate Map")]
    public void GenerateMap()
    {
        mapReader.SetMap(mapName);
        mapGenerator.GenerateMap(mapReader.GetMapData(), mapReader.GetDictionaryOfPrefabs(), tileDisplacement);
    }
}
