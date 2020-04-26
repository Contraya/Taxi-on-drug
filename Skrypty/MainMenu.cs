﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject car;
    // Start is called before the first frame update
    void Start()
    {
       car = GameObject.FindGameObjectWithTag("Player");
    }

    public void toGame()
    {
        car.GetComponent<CarController>().enabled = false;
        car.GetComponent<CarUserControll>().enabled = false;
        car.GetComponent<CarAudio>().enabled = false;
        car.GetComponent<CarTuning>().enabled = false;
        car.transform.Rotate(0,0,0);
        SceneManager.LoadScene("Clients");
    }
    public void Client1(){
        
    }
    public void toTuning(){
        car.GetComponent<CarController>().enabled = false;
        car.GetComponent<CarUserControll>().enabled = false;
        car.GetComponent<CarAudio>().enabled = false;
        car.GetComponent<CarTuning>().enabled = true;
        SceneManager.LoadScene("Tuning");
    }
}