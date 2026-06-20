using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseEndingUI : MonoBehaviour
{
    [SerializeField] private GameObject loseScreen1;
    [SerializeField] private GameObject loseScreen2;
    [SerializeField] private GameObject loseScreen3;

    public void ShowLoseEnding()
    {
        loseScreen1.SetActive(true);
        loseScreen2.SetActive(false);
        loseScreen3.SetActive(false);
    }

    public void ShowLoseScreen2()
    {
        loseScreen1.SetActive(false);
        loseScreen2.SetActive(true);
        loseScreen3.SetActive(false);
    }

    public void ShowLoseScreen3()
    {
        loseScreen1.SetActive(false);
        loseScreen2.SetActive(false);
        loseScreen3.SetActive(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("HomePage");
    }
}