using UnityEngine;

public class IntroFlowController : MonoBehaviour
{
    [SerializeField] private GameObject storyPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject gameplayUI;

    private void Start()
    {
        storyPanel.SetActive(true);
        tutorialPanel.SetActive(false);

        if (gameplayUI != null)
            gameplayUI.SetActive(false);

        if (GameManager.Instance != null)
            GameManager.Instance.SetState(GameState.Tutorial);
    }

    public void ShowTutorial()
    {
        storyPanel.SetActive(false);
        tutorialPanel.SetActive(true);
    }

    public void BeginDayOne()
    {
        tutorialPanel.SetActive(false);

        if (gameplayUI != null)
            gameplayUI.SetActive(true);

        if (GameManager.Instance != null)
            GameManager.Instance.SetState(GameState.Playing);
    }
}