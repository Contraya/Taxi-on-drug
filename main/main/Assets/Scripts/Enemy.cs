﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rb;
    List<GameObject>[] activeEnemies; // lista wszystkich torów, pobierana z EnemyManager
    private List<GameObject> myTrack; // lista wszystkich modeli na torze po którym porusza się model
    public float speed; // prędkość modelu, randomowa
    private float track; // numer toru
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        activeEnemies = GameObject.Find("EnemyManager").GetComponent<EnemyManager>().activeEnemies;
        speed = Random.Range(15f, 30f);
        rb.velocity = Vector3.forward * speed;
        track = transform.position.x;
        switch (track)
        {
            case -3.8f: myTrack = activeEnemies[0]; break;
            case -1.22f: myTrack = activeEnemies[1]; break;
            case 1.25f: myTrack = activeEnemies[2]; break;
            case 3.86f: myTrack = activeEnemies[3]; break;
        }
    }

    void Update()
    {
        // jeśli na torze jest tylko jeden model jego prędkość jest stała, jeśli jest więcej jego prędkość zależy od innych modeli
        speed = CapSpeed();
        rb.velocity = rb.velocity.normalized * speed;
    }

    // jeśli model zostanie uderzony przez gracza będzie starał się utrzymac swój pasu ruchu
    void FixedUpdate()
    {
        Vector3 target = new Vector3(track, rb.position.y, rb.position.z);
        Vector3 toTarget = target - rb.position;
        rb.MovePosition(target);
    }

    // funkcja odpowiedzialna za eliminację zderzeń modeli na torzem
    // jesli model dogoni inny model znajdujący się przed nim i odległość między nimi wynosi 7 jednostek, jego prędkość jest równa prędkości modelu z przodu
    private float CapSpeed()
    {
        int enemyInFront = myTrack.IndexOf(this.gameObject);
        for (int i = 0; i < myTrack.Count; i++)
        {
            if (myTrack[i].transform.position.z - transform.position.z <= 0) continue;
            if (transform.position.z + 7f >= myTrack[i].transform.position.z)
            {
                enemyInFront = i;
                break;
            }
        }
        if (enemyInFront != myTrack.IndexOf(this.gameObject))
        {
            return myTrack[enemyInFront].GetComponent<Enemy>().speed;
        }
        else
        {
            return speed;
        }
    }
}
