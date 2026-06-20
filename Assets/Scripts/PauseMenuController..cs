using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public void ResumeGame()
    {
        SceneManager.LoadScene("MainStation");
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("HomePage");
    }
}