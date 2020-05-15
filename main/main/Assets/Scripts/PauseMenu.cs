using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenu;
    public GameObject ui;
    public AudioListener sound;
    GameObject car;
    void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player");
        ui = GameObject.Find("Canvas");
        sound = GameObject.Find("Camera").GetComponent<AudioListener>();
        pauseMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        ui.SetActive(true);
        Time.timeScale = 1f;
        gamePaused = false;
        sound.enabled = true;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        ui.SetActive(false);
        Time.timeScale = 0f;
        gamePaused = true;
        sound.enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        string filePath = EditorUtility.SaveFilePanel("Save Game", Application.dataPath + Path.AltDirectorySeparatorChar + "GameSaves", "gamesave.json", "json");
        Save save = car.GetComponent<CarController>().createSaveGame();
        BinaryFormatter bf = new BinaryFormatter();
        string json = JsonUtility.ToJson(save);
        FileStream file = File.Create(filePath);
        bf.Serialize(file, json);
        file.Close();
        Debug.Log("Game Saved");
    }
}
