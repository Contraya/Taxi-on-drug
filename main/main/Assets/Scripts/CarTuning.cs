﻿using UnityEngine;

public class CarTuning : MonoBehaviour
{
    public Transform car;
    public float rotationSpeed = 25.0f;

    public GameObject[] FrontBumper; //tablica elementów
    public GameObject[] RearBumper;//tablica elementów
    public GameObject[] Engine;//tablica elementów
    public GameObject[] Wheel;//tablica elementów (podajesz dla 3 kół różnych kół według wzoru 3 * 4)

    public int FrontBumperId;//Id wybranego zdrzaka
    public int RearBumperId;//Id wybranego zdrzaka
    public int EngineId;//Id wybranego Silnika
    public int WheelId;//Id wybrane koła
    public bool rotateCar = false;
    // Start is called before the first frame update
    void Start()
    {  
        //żeby nie niszczoło obiektu między scenami
        DontDestroyOnLoad(car);
    }

    // Update is called once per frame
    public void UpdateOnce(){
        for (int i = 0; i < FrontBumper.Length; i++){
            if(i != FrontBumperId) FrontBumper[i].SetActive(false);
            else FrontBumper[i].SetActive(true);
        }
        //szukanie tylnego zderzaka
        for (int i = 0; i < RearBumper.Length; i++){
            if(i != RearBumperId) RearBumper[i].SetActive(false);
            else RearBumper[i].SetActive(true);
        }
        //szukanie Sinlika
        for (int i = 0; i < Engine.Length; i++){
            if(i != EngineId) Engine[i].SetActive(false);
            else Engine[i].SetActive(true);
        }
        //Szukanie koła
        for (int i = 0; i < Wheel.Length; i+=4){
            if(i != WheelId){
                for(int j = 0; j < 4; j++)
                    Wheel[i+j].SetActive(false);
            }
            else{
                for(int j = 0; j < 4; j++)
                    Wheel[i+j].SetActive(true);
            }
        }
    }
    void Update()
    {
        //Sprawdzanie czy ma się obracać wraz z obliczeniem szybkości obrotu
        if(rotateCar)car.Rotate(Vector3.up * ( rotationSpeed * Time.deltaTime));
    }
    //Zwiększenie indeksu Przedniego zderzaka
    public void AddFrontBumper(){
        if(FrontBumperId < FrontBumper.Length - 1) FrontBumperId++;
        else FrontBumperId = 0;
        UpdateOnce();
    }
    //Zmniesznie indeksu Przedniego zderzaka
    public void TakeFrontBumper(){
        FrontBumperId = (FrontBumperId + (FrontBumper.Length - 1)) % (FrontBumper.Length);
        UpdateOnce();
    }
    //Zwiększanie indeksu Tylnego zderzaka
    public void AddRearBumper(){
        if(RearBumperId < FrontBumper.Length - 1) RearBumperId++;
        else RearBumperId = 0;
        UpdateOnce();
    }
    //Zmniesznie indeksu Tylnego zderzaka
    public void TakeRearBumper(){
        RearBumperId = (RearBumperId + (RearBumper.Length - 1)) % (RearBumper.Length);
        UpdateOnce();
    }
    //Zwiększanie indeksu Silnika
    public void AddEngine(){
        if(EngineId < Engine.Length - 1) EngineId++;
        else EngineId = 0;
        UpdateOnce();
    }
    //Zmniesznie indeksu Silnika
    public void TakeEngine(){
        EngineId = (EngineId + (Engine.Length - 1)) % (Engine.Length);
        UpdateOnce();
    }
    //Zwiększanie indeksu Koła
    public void AddWheel(){
        if(WheelId < Wheel.Length - 4) WheelId+=4;
        else WheelId = 0; 
        UpdateOnce();
    }
    //Zmaniesznie indeksu Koła
    public void TakeWheel(){
        WheelId = (WheelId + Wheel.Length - 4) % (Wheel.Length);
        UpdateOnce();
    }
    //Zmiana statusu obrotu
    public void SetCarRotation(){
        rotateCar = !rotateCar;
    }
}
