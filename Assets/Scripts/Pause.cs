using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioSource backgroundMusic, Main;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                backgroundMusic.enabled = false;
                Main.enabled = false;   

            }
            else
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                backgroundMusic.enabled = true;
                Main.enabled = true;
            }
        }
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        backgroundMusic.enabled = true;
        Main.enabled = true;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
