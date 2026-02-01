using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorial, ButtonNext, ButtonPrevious, ButtonOkay, Panel1, Panel2;
    
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
        TutorialPreviousPanel();
    }

    public void TutorialNextPanel()
    {
        Panel2.SetActive(true);
        ButtonOkay.SetActive(true);
        ButtonPrevious.SetActive(true);
        Panel1.SetActive(false);
        ButtonNext.SetActive(false);
    }

    public void TutorialPreviousPanel()
    {
        Panel1.SetActive(true);
        ButtonNext.SetActive(true);
        Panel2.SetActive(false);
        ButtonPrevious.SetActive(false);
    }

}
