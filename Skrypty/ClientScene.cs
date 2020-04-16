//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using System.IO;

public class ClientScene : MonoBehaviour
{
    private Text Client1;
    private Text Client2;
    private Text Client3;
    public TextAsset TextFile;
    private GameObject car;
    private string StringText;//
    private List<string> eachLine;
    private string Client1Name;
    private string Client2Name;
    private string Client3Name;
    private float Client1Cash;
    private float Client2Cash;
    private float Client3Cash;
    private float Client1Distance;
    private float Client2Distance;
    private float Client3Distance;



    // Start is called before the first frame update
    void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player");
        Client1 = GameObject.Find("Canvas/Client1").GetComponent<Text>();
        Client2 = GameObject.Find("Canvas/Client2").GetComponent<Text>();
        Client3 = GameObject.Find("Canvas/Client3").GetComponent<Text>();
        //czytanie z pliku
        StringText = TextFile.text;
        eachLine = new List<string>();
        eachLine.AddRange(StringText.Split("\n"[0]));
        //długość pliku
        int kWords = eachLine.Count;
        //Debug.Log(eachLine[kWords-1]);
        Client1Name = eachLine[Random.Range(0,kWords-1)];
        Client2Name = eachLine[Random.Range(0,kWords-1)];
        Client3Name = eachLine[Random.Range(0,kWords-1)];
        Client1Distance = Random.Range(100.0f,150.0f);
        Client2Distance = Random.Range(1000.0f,15000.0f);
        Client3Distance = Random.Range(1000.0f,15000.0f);
        Client1Cash = Client1Distance * Random.Range(0.1f,0.6f);
        Client2Cash = Client2Distance * Random.Range(0.1f,0.6f);
        Client3Cash = Client3Distance * Random.Range(0.1f,0.6f);
        
        Client1.text = "Client \t:\t"+Client1Name+"\n"+"Cash \t:\t"+Client1Cash.ToString()+"\n"+"Distance :\t"+Client1Distance.ToString()+"m";
        Client2.text = "Client \t:\t"+Client2Name+"\n"+"Cash \t:\t"+Client2Cash.ToString()+"\n"+"Distance :\t"+Client2Distance.ToString()+"m";
        Client3.text = "Client \t:\t"+Client3Name+"\n"+"Cash \t:\t"+Client3Cash.ToString()+"\n"+"Distance :\t"+Client3Distance.ToString()+"m";
    }
    public void Clinet1(){
        car.GetComponent<CarController>().enabled = true;
        car.GetComponent<CarUserControll>().enabled = true;
        car.GetComponent<CarAudio>().enabled = true;
        car.GetComponent<CarTuning>().enabled = false;
        car.GetComponent<CarController>().ClinetCash = Client1Cash;
        car.GetComponent<CarController>().ClientName = Client1Name;
        car.GetComponent<CarController>().goalDistance = Client1Distance;
        car.GetComponent<CarController>().Travel = false;
        car.GetComponent<CarController>().pivot = true;
        car.GetComponent<CarController>().distanceTravelled = 0;
        SceneManager.LoadScene("Game");
    }
    public void Clinet2(){
        car.GetComponent<CarController>().enabled = true;
        car.GetComponent<CarUserControll>().enabled = true;
        car.GetComponent<CarAudio>().enabled = true;
        car.GetComponent<CarTuning>().enabled = false;
        car.GetComponent<CarController>().ClinetCash = Client2Cash;
        car.GetComponent<CarController>().ClientName = Client2Name;
        car.GetComponent<CarController>().goalDistance = Client2Distance;
        car.GetComponent<CarController>().Travel = false;
        car.GetComponent<CarController>().pivot = true;
        SceneManager.LoadScene("Game");
    }
    public void Clinet3(){
        car.GetComponent<CarController>().enabled = true;
        car.GetComponent<CarUserControll>().enabled = true;
        car.GetComponent<CarAudio>().enabled = true;
        car.GetComponent<CarTuning>().enabled = false;
        car.GetComponent<CarController>().ClinetCash = Client3Cash;
        car.GetComponent<CarController>().ClientName = Client3Name;
        car.GetComponent<CarController>().goalDistance = Client3Distance;
        car.GetComponent<CarController>().Travel = false;
        car.GetComponent<CarController>().pivot = true;
        SceneManager.LoadScene("Game");
    }
}
