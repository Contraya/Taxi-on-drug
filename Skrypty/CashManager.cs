using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    
    public GameObject cashPrefab;
    private Transform playerTransform;
    private List<GameObject> activeCash;
    private float spawnZ = 100; 

    void Start()
    {
        activeCash = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnCash();        
    }

    void Update()
    {
        spawnZ = playerTransform.transform.position.z + 200;
        if( activeCash.Count <= 2) {
            spawnZ = activeCash[activeCash.Count-1].transform.position.z + Random.Range(30, 500);
            SpawnCash();
        }
        for (int i = 0; i < activeCash.Count-1; i++){
            activeCash[i].transform.Rotate(Vector3.forward * (100f * Time.deltaTime));
        }
        if( playerTransform.position.z - 5f > activeCash[0].transform.position.z){
            DeleteCash();
        }
    }

    void SpawnCash(){
        GameObject cash;
        cash = Instantiate(cashPrefab) as GameObject;
        cash.transform.SetParent(transform);
        cash.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), 0.2f, 1 * spawnZ);
        activeCash.Add(cash);
    }

    public void DeleteCash(GameObject cash = null){
        if (cash != null){
            Destroy(cash);
            activeCash.Remove(cash);
        } else {
            Destroy(activeCash[0]);
            activeCash.RemoveAt(0);
        }
    }
}
