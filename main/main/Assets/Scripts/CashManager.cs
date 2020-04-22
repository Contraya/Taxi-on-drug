using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    public GameObject[] cashPrefab = new GameObject[2]; // lista modeli pieniedzy
    private Transform playerTransform; // pozycja gracza
    private List<GameObject> activeCash; // lista z aktywnymi obiektami pieniedzy na scenie
    private float spawnZ = 100; 

    void Start()
    {
        activeCash = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnCash();        
    }

    void Update()
    {   
        // maksymalna ilosc modeli kasy na scenie wynosi 2, pojawia sie w odleglosci randowmowej od 300 do 1000 jednostek dalej niz ostatni aktywny model
        // ten zakres okresla rzadkosc wystepowania kasy na scenie
        if( activeCash.Count <= 2) {
            spawnZ = activeCash[activeCash.Count-1].transform.position.z + Random.Range(300, 1000);
            SpawnCash();
        }
        // petla odpowiedzialna za obracanie sie modeli
        for (int i = 0; i < activeCash.Count-1; i++){
            activeCash[i].transform.Rotate(Vector3.forward * (100f * Time.deltaTime));
        }
        // jesli model kasy zostanie ominiety i odleglosc za graczem przekroczy 5 jednostek model jest usuwany ze sceny oraz z listy aktywnych modeli
        if( playerTransform.position.z - 5f > activeCash[0].transform.position.z){
            DeleteCash();
        }
    }

    // funkcja spawnowania kasy, randomowo wybierany jest jeden z modeli, model pojawia sie z randomowa pozycja na osi x trasy
    void SpawnCash(){
        GameObject cash;
        int r = Random.Range(0, 2);
        float posY = 0.2f;
        if(r == 1){
            posY = 0.5f;
        }
        cash = Instantiate(cashPrefab[r]) as GameObject;
        cash.transform.SetParent(transform);
        cash.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), posY, spawnZ);
        activeCash.Add(cash);
    }

    // funkcja usuwania modeli ze sceny, usuniety moze byc model miniety lub podany do funkcji
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
