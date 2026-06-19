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

    [Header("Game State")]
    public GameState CurrentState;

    [Header("Progress")]
    [SerializeField] private int currentDay = 1;
    [SerializeField] private int finalDay = 15;
    [SerializeField] private int requiredContacts = 5;
    [SerializeField] private GameObject dayCompleteScreen;
    
    [SerializeField] private DayCompleteUI dayCompleteUI;

    [Header("Debug Controls")]
    [SerializeField] private KeyCode nextDayKey = KeyCode.N;

    private bool gameEnded = false;

    private int confirmedContacts = 0;

    public int CurrentDay => currentDay;
    public int FinalDay => finalDay;
    public int RequiredContacts => requiredContacts;
    public int ConfirmedContacts => confirmedContacts;
    public bool IsGameEnded => gameEnded;

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
        dayCompleteScreen.SetActive(false);
        SetState(GameState.Playing);

        Debug.Log("Spiel gestartet.");
        Debug.Log("Tag: " + currentDay + " / " + finalDay);
        Debug.Log("Ziel: " + requiredContacts + " Kontakte.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(nextDayKey))
        {
            EndCurrentDay();
        }
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

    public void OnConfirmedContactAdded()
    {
        confirmedContacts++;

        Debug.Log(
            "Kontakt bestätigt: " +
            confirmedContacts +
            " / " +
            requiredContacts);

        CheckStoryProgression();
        CheckWinCondition();
    }

public void EndCurrentDay()
{
    if (gameEnded)
        return;

    if (dayCompleteUI != null)
    {
        dayCompleteUI.Refresh();
    }

    dayCompleteScreen.SetActive(true);

    SetState(GameState.LogbookOpen);
}

    private void CheckWinCondition()
    {
        if (gameEnded)
            return;

        if (confirmedContacts >= requiredContacts)
        {
            TriggerWin();
        }
    }

    private void CheckLoseCondition()
    {
        if (gameEnded)
            return;

        TriggerLose();
    }

    private void TriggerWin()
    {
        gameEnded = true;

        SetState(GameState.EndingWin);

        Debug.Log("WIN");
    }

    private void TriggerLose()
    {
        gameEnded = true;

        SetState(GameState.EndingLose);

        Debug.Log("LOSE");
    }

    private void CheckStoryProgression()
    {
        if (StoryEventManager.Instance == null)
            return;

        StoryEventManager.Instance.CheckStoryEvents(
            currentDay,
            confirmedContacts);
    }
    [SerializeField] private int transmissionsPerDay = 3;

private int transmissionsUsedToday = 0;

public int AttemptsLeftToday
{
    get { return transmissionsPerDay - transmissionsUsedToday; }
}

public void OnTransmissionSent()
{
    transmissionsUsedToday++;

    if (transmissionsUsedToday >= transmissionsPerDay)
    {
        transmissionsUsedToday = 0;
        EndCurrentDay();
    }
}

public void StartNextDay()
{
     

    
    dayCompleteScreen.SetActive(false);

    currentDay++;

    if (currentDay > finalDay)
    {
        CheckLoseCondition();
        return;
    }

    SetState(GameState.Playing);

    CheckStoryProgression();
}
}