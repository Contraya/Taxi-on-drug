using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Skrypt dla Player
public class CollectCoin : MonoBehaviour
{
    public Text txt;
    public int currentScore = 2000;
    // Start is called before the first frame update
    void Start()
    {
        txt.text="PLN : " + currentScore;
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Coin1") {
            currentScore += 5;
            txt.text = "PLN : " + currentScore; 
        }else if(other.tag == "Coin2"){
            currentScore += 200;
            txt.text = "PLN : " + currentScore;
        }
    }
    public void addMoney(){
        currentScore += 1000;
        txt.text = "PLN : " + currentScore;
    }
}

