using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    public GameObject car;
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
        car.transform.Rotate(0, 0, 0);
        SceneManager.LoadScene("Clients");
    }
    public void toTuning()
    {
        car.GetComponent<CarController>().enabled = false;
        car.GetComponent<CarUserControll>().enabled = false;
        car.GetComponent<CarAudio>().enabled = false;
        car.GetComponent<CarTuning>().enabled = true;
        SceneManager.LoadScene("Tuning");
    }

    public void LoadGame()
    {
        string filePath = EditorUtility.OpenFilePanel("Load Game", Application.dataPath + Path.AltDirectorySeparatorChar + "GameSaves", "json");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(filePath, FileMode.Open);
        string json = (string)bf.Deserialize(file);
        Save save = JsonUtility.FromJson<Save>(json);
        file.Close();
        car.GetComponent<CarTuning>().FrontBumperId = save.frontBumper;
        car.GetComponent<CarTuning>().RearBumperId = save.rearBumber;
        car.GetComponent<CarTuning>().WheelId = save.wheels;
        car.GetComponent<CarTuning>().EngineId = save.engine;
        car.GetComponent<CarController>().cash = save.cash;
    }
}
