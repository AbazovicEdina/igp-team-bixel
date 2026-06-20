using UnityEngine;
using UnityEngine.SceneManagement;

public class MainStationPause : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("PauseMenu");
        }
    }
}