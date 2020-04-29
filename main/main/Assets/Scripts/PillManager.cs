using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillManager : MonoBehaviour
{
    public GameObject[] pillPrefab = new GameObject[1];
    private Transform playerTransform; // pozycja gracza
    private List<GameObject> activePill; // lista z aktywnymi obiektami pieniedzy na scenie
    private float spawnZ = 200;

    void Start()
    {
        activePill = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnPill();
    }

    void Update()
    {
        // maksymalna ilosc modeli kasy na scenie wynosi 2, pojawia sie w odleglosci 1000 jednostek dalej niz ostatni aktywny model
        // ten zakres okresla rzadkosc wystepowania kasy na scenie
        if (activePill.Count <= 2)
        {
            spawnZ = activePill[activePill.Count - 1].transform.position.z + 1000;
            SpawnPill();
        }
        // petla odpowiedzialna za obracanie sie modeli
        for (int i = 0; i < activePill.Count - 1; i++)
        {
            activePill[i].transform.Rotate(Vector3.up * (100f * Time.deltaTime));
        }
        // jesli model kasy zostanie ominiety i odleglosc za graczem przekroczy 5 jednostek model jest usuwany ze sceny oraz z listy aktywnych modeli
        if (playerTransform.position.z - 10f > activePill[0].transform.position.z)
        {
            DeletePill();
        }
    }

    // funkcja spawnowania kasy, randomowo wybierany jest jeden z modeli, model pojawia sie z randomowa pozycja na osi x trasy
    void SpawnPill()
    {
        GameObject pill;
        pill = Instantiate(pillPrefab[0]) as GameObject;
        pill.transform.SetParent(transform);
        pill.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), 0.5f, spawnZ);
        activePill.Add(pill);
    }

    // funkcja usuwania modeli ze sceny, usuniety moze byc model miniety lub podany do funkcji
    public void DeletePill(GameObject pill = null)
    {
        if (pill != null)
        {
            Destroy(pill);
            activePill.Remove(pill);
        }
        else
        {
            Destroy(activePill[0]);
            activePill.RemoveAt(0);
        }
    }
}
