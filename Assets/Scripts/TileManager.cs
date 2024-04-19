using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject[] tiles2;
    public int zSpawn = 0;
    public int tileLength = 14;
    public int numberOfTiles = 5;
    [SerializeField] private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;

    public int tileCount;
    public int tile2Count;

    private void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(0, tiles.Length));
            }
        }
    }

    private void Update()
    {
        if (playerTransform.position.z - 14 > zSpawn - (numberOfTiles * tileLength))
        {
            if(tileCount <= 30)
            {
                SpawnTile(Random.Range(0, tiles.Length));
                DeleteTile();
            }
            else if (tile2Count <= 30 && tileCount >= 30) 
            {
                SpawnTile2(Random.Range(0, tiles2.Length));
                DeleteTile();
            }
            else
            {
                tileCount = 0;
                tile2Count = 0;
            }
            
        }
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tiles[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
        tileCount++;
    }

    private void SpawnTile2(int tileIndex2)
    {
        GameObject go = Instantiate(tiles2[tileIndex2], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
        tile2Count++;
        
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
