using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorial;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
        
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleTutorial()
    {
        tutorial.SetActive(!tutorial.activeInHierarchy);
    }
}
