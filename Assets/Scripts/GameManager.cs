using UnityEngine;

public enum GameState
{
    Boot,
    Tutorial,
    Playing,
    Transmitting,
    Receiving,
    LogbookOpen,
    EndingWin,
    EndingLose
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState CurrentState;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        CurrentState = GameState.Playing;
    }

    public bool CanAcceptGameplayInput()
    {
        return CurrentState == GameState.Playing ||
               CurrentState == GameState.Tutorial;
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;
    }
}