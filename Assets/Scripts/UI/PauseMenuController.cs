using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;

    private GameState previousState;
    private bool isPaused = false;

    private void Start()
    {
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        if (GameManager.Instance == null)
            return;

        previousState = GameManager.Instance.CurrentState;

        pauseMenuPanel.SetActive(true);
        GameManager.Instance.SetState(GameState.LogbookOpen);

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);

        if (GameManager.Instance != null)
            GameManager.Instance.SetState(previousState);

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenOptions()
    {
    Time.timeScale = 1f;
    PlayerPrefs.SetString("OptionsReturnScene", "MainStation");
    SceneManager.LoadScene("OptionsMenu");
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HomePage");
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}