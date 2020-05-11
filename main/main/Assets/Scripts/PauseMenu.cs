using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenu;
    public GameObject ui;
    public AudioListener sound;

    void Start()
    {
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

    void Resume()
    {
        pauseMenu.SetActive(false);
        ui.SetActive(true);
        Time.timeScale = 1f;
        gamePaused = false;
        sound.enabled = true;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        ui.SetActive(false);
        Time.timeScale = 0f;
        gamePaused = true;
        sound.enabled = false;
    }
}
