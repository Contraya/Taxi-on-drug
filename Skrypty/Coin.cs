using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Skrypt dla monety
public class Coin : MonoBehaviour
{
    public Transform Coins;
    public bool rotate = true;
    public float rotationSpeed = 125.0f;
   
    // Update is called once per frame
    void Update()
    {
        if(rotate)Coins.Rotate(Vector3.forward * ( rotationSpeed * Time.deltaTime));
        
    }
     void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player") {
            Destroy(gameObject);
        }
    }
}

