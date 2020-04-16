//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    private GameObject car;
    // Start is called before the first frame update
    void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player");
        car.GetComponent<CarController>().text = GameObject.Find("Text").GetComponent<Text>();
        car.GetComponent<CarController>().distance = GameObject.Find("DistanceText").GetComponent<Text>();
        car.GetComponent<CarController>().cashManager = GameObject.Find("CashManager").GetComponent<CashManager>();
    }
}
