using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;

    private Transform playerTransform;
    private float spawnZ = 0.0f;
    private float tileLength = 30.0f;
    private int amountOfTiles = 7;

    // Start is called before the first frame update
    void Start()
    {
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
        if ( playerTransform.position.z > (spawnZ - amountOfTiles * tileLength))
        {
            Debug.Log(playerTransform.position.z);
            Debug.Log(spawnZ - amountOfTiles * tileLength);
            SpawnTile();
        }
    }

    // Function to create a new road prefab
    void SpawnTile(int prefabIndex = -1)
	{
        // init object
        GameObject go;
        // choose random one from prefab list and make copy of it
        go = Instantiate(tilePrefabs[Random.Range(0, tilePrefabs.Length)]) as GameObject;
        // set Parent to the copy 
        go.transform.SetParent(transform);
        // move forward new prefab
        go.transform.position = Vector3.forward * spawnZ;
        // increase next spawn point by length of our road (30)
        spawnZ += tileLength;
	}
}
