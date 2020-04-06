using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    List<GameObject> objectsPlaced = new List<GameObject>();
    public void GenerateMap(int[,] mapData, Dictionary<TileType, GameObject> prefabsById, int displacement)
    {
        if (objectsPlaced.Count != 0)
        {
            foreach (GameObject GO in objectsPlaced)
            {
                
                Destroy(GO);
            }

        }


        int currentTile = 0;
        GameObject currentPrefab;
        TileType tileType;
        int yLength = mapData.GetLength(1);
        int xLength = mapData.GetLength(0);
        for (int y = 0; y < yLength; y++)
        {
            for (int x = 0; x < xLength; x++)
            {
                currentTile = mapData[x, y];
                tileType = TileMethods.TypeById[currentTile];
                currentPrefab = prefabsById[tileType];
                
                objectsPlaced.Add(GameObject.Instantiate(currentPrefab, new Vector3Int(x * displacement, 0, y * displacement), Quaternion.identity));
            }
        }
    }
}
