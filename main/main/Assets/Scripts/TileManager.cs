using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;

    private Transform playerTransform;
    private float spawnZ = -30.0f;
    private float tileLength = 30.0f;
    private int amountOfTiles = 7;

    private float safeZone = 40.0f;

    private int LastPrefab = 0;

    private List<GameObject> activeTiles;

    // Start is called before the first frame update
    void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i=0; i < amountOfTiles; i++)
        {
            SpawnTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // create new road prefab if player position 
        if ( playerTransform.position.z - safeZone > (spawnZ - amountOfTiles * tileLength))
        {
            //Debug.Log(playerTransform.position.z);
            //Debug.Log(spawnZ - amountOfTiles * tileLength);
            SpawnTile();
            DeleteTile();
        }
    }

    // Function to create a new road prefab
    void SpawnTile(int prefabIndex = -1)
	{
        // init object
        GameObject go;
        // choose random one from prefab list and make copy of it
        go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        // set Parent to the copy 
        go.transform.SetParent(transform);
        // move forward new prefab
        go.transform.position = Vector3.forward * spawnZ;
        // increase next spawn point by length of our road (30)
        spawnZ += tileLength;
        activeTiles.Add(go);
	}

    // Function to delete tile behind player
    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = LastPrefab;

        while (randomIndex == LastPrefab)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        LastPrefab = randomIndex;
        return randomIndex;
    }
}
