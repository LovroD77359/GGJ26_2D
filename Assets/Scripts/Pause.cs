using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public Canvas pauseMenu;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 0f;
                pauseMenu.enabled = true;
            }
            else
            {
                Time.timeScale = 1f;
                pauseMenu.enabled = false;
            }
        }
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        pauseMenu.enabled = false;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        pauseMenu.enabled = false;
        SceneManager.LoadScene("MainMenu");
    }
}
