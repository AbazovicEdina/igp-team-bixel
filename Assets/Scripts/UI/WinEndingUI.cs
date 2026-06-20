using UnityEngine;
using UnityEngine.SceneManagement;

public class WinEndingUI : MonoBehaviour
{
    [SerializeField] private GameObject winScreen1;
    [SerializeField] private GameObject winScreen2;
    [SerializeField] private GameObject winScreen3;

    public void ShowWinEnding()
    {
        winScreen1.SetActive(true);
        winScreen2.SetActive(false);
        winScreen3.SetActive(false);
    }

    public void ShowWinScreen2()
    {
        winScreen1.SetActive(false);
        winScreen2.SetActive(true);
        winScreen3.SetActive(false);
    }

    public void ShowWinScreen3()
    {
        winScreen1.SetActive(false);
        winScreen2.SetActive(false);
        winScreen3.SetActive(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("HomePage");
    }
}