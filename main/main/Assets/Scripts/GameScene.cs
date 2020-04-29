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
        car.GetComponent<CarController>().healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        car.GetComponent<CarController>().enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        car.GetComponent<CarController>().healthBar.setMaxValue(car.GetComponent<CarController>().maxHealth);
        car.GetComponent<CarController>().pillManager = GameObject.Find("PillManager").GetComponent<PillManager>();
        car.GetComponent<CarController>().health = car.GetComponent<CarController>().maxHealth;
    }
}
