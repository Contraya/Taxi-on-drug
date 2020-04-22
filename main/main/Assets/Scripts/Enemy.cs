using System.Collections;
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
        if (myTrack.Count <= 1)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
        else
        {
            rb.velocity = CapSpeed();
        }
    }

    // jeśli model zostanie zepchnięty z toru przez gracza, po pewnym czasie zaczyna na niego wracać
    private void LateUpdate()
    {
        Vector3 target = new Vector3(track, transform.position.y, transform.position.z);
        Vector3 toTarget = target - transform.position;
        if (toTarget.magnitude > 0.15f)
        {
            transform.Translate(toTarget.normalized * 1.5f * Time.deltaTime);
        }
    }

    // funkcja odpowiedzialna za eliminację zderzeń modeli na torzem
    // jesli model dogoni inny model znajdujący się przed nim i odległość między nimi wynosi 10 jednostek, jego prędkość jest równa prędkości modelu z przodu
    private Vector3 CapSpeed()
    {
        int enemyInFront = myTrack.IndexOf(this.gameObject);
        for (int i = 0; i < myTrack.Count; i++)
        {
            if(myTrack[i].transform.position.z - transform.position.z < 0) continue;
            if (transform.position.z + 10f >= myTrack[i].transform.position.z)
            {
                enemyInFront = i;
                break;
            }
        }
        if (enemyInFront != myTrack.IndexOf(this.gameObject))
        {
            return rb.velocity.normalized * myTrack[enemyInFront].GetComponent<Enemy>().speed;
        }
        else
        {
            return (rb.velocity.normalized * speed);
        }
    }
}
