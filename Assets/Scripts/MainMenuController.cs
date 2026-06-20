using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MainStation");
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("HomePage");
    }

    public void ExitGame()
    {
        Debug.Log("Quit Game clicked");
        Application.Quit();
    }
}