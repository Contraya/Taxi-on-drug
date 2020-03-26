using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CarTuning : MonoBehaviour
{
    public Transform car;
    public float rotationSpeed = 25.0f;

    public GameObject[] FrontBumper;
    public GameObject[] RearBumper;
    public GameObject[] Engine;
    public GameObject[] Wheel;

    public int FrontBumperId;
    public int RearBumperId;
    public int EngineId;
    public int WheelId;
    public bool rotateCar;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < FrontBumper.Length; i++){
            if(FrontBumper[i].activeSelf){
                FrontBumperId = i;
            }
            if( i >= FrontBumper.Length){
                i = 0;
            }
        }    
        for (int i = 0; i < RearBumper.Length; i++){
            if(RearBumper[i].activeSelf){
                RearBumperId = i;
            }
            if( i >= RearBumper.Length){
                i = 0;
            }
        } 
        for (int i = 0; i < Engine.Length; i++){
            if(Engine[i].activeSelf){
                EngineId = i;
            }
            if( i >= Engine.Length){
                i = 0;
            }
        }
        for (int i = 0; i < Wheel.Length; i++){
            if(Wheel[i].activeSelf){
                WheelId = i;
            }
            if( i >= Wheel.Length){
                i = 0;
            }
        } 
        DontDestroyOnLoad(car);
    }

    // Update is called once per frame
    void Update()
    {
        if(rotateCar)car.Rotate(Vector3.up * ( rotationSpeed * Time.deltaTime));

        for (int i = 0; i < FrontBumper.Length; i++){
            if(i != FrontBumperId) FrontBumper[i].SetActive(false);
            //else if( i >= FrontBumper.Length) i = 0;
            else FrontBumper[i].SetActive(true);
        }
        for (int i = 0; i < RearBumper.Length; i++){
            if(i != RearBumperId) RearBumper[i].SetActive(false);
            //else if( i >= RearBumper.Length) i = 0;
            else RearBumper[i].SetActive(true);
        }
        for (int i = 0; i < Engine.Length; i++){
            if(i != EngineId) Engine[i].SetActive(false);
            else Engine[i].SetActive(true);
        }
        for (int i = 0; i < Wheel.Length; i+=4){
            //Debug.Log(i);
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
    //Zwiększenie indeksu Przedniego zderzaka
    public void AddFrontBumper(){
        if(FrontBumperId < FrontBumper.Length - 1) FrontBumperId++;
        else FrontBumperId = 0;
    }
    //Zmniesznie indeksu Przedniego zderzaka
    public void TakeFrontBumper(){
        FrontBumperId = (FrontBumperId + 1) % (FrontBumper.Length - 1);
    }
    //Zwiększanie indeksu Tylnego zderzaka
    public void AddRearBumper(){
        if(RearBumperId < FrontBumper.Length - 1) RearBumperId++;
        else RearBumperId = 0;
    }
    //Zmniesznie indeksu Tylnego zderzaka
    public void TakeRearBumper(){
        RearBumperId = (RearBumperId + 1) % (RearBumper.Length - 1);
    }
    //Zwiększanie indeksu Silnika
    public void AddEngine(){
        if(EngineId < Engine.Length - 1) EngineId++;
        else EngineId = 0;
    }
    //Zmniesznie indeksu Silnika
    public void TakeEngine(){
        EngineId = (EngineId + 1) % (Engine.Length - 1);
    }
    //Zwiększanie indeksu Koła
    public void AddWheel(){
        if(WheelId < Wheel.Length - 4) WheelId+=4;
        else WheelId = 0; 
    }
    //Zmaniesznie indeksu Koła
    public void TakeWheel(){
        WheelId = (WheelId + 4) % (Wheel.Length);
    }
    public void SetCarRotation(){
        rotateCar = !rotateCar;
    }
}
