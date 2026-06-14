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
    [SerializeField] private int requiredContacts = 20;

    [Header("Debug Controls")]
    [SerializeField] private KeyCode nextDayKey = KeyCode.N;

    private bool gameEnded = false;

    public int CurrentDay
    {
        get { return currentDay; }
    }

    public int FinalDay
    {
        get { return finalDay; }
    }

    public int RequiredContacts
    {
        get { return requiredContacts; }
    }

    public int ConfirmedContacts
    {
        get
        {
            if (LogbookManager.Instance == null)
            {
                return 0;
            }

            return LogbookManager.Instance.ConfirmedContactCount;
        }
    }

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
        SetState(GameState.Playing);

        Debug.Log("Spiel gestartet.");
        Debug.Log("Tag: " + currentDay + " / " + finalDay);
        Debug.Log("Ziel: " + requiredContacts + " bestätigte Kontakte.");
    }

    private void Update()
    {
        // Nur zum Testen:
        // Mit N springst du zum nächsten Tag.
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
        Debug.Log("GameState geändert zu: " + CurrentState);
    }

    public void OnConfirmedContactAdded()
    {
        Debug.Log("GameManager: Neuer bestätigter Kontakt.");
        Debug.Log("Fortschritt: " + ConfirmedContacts + " / " + requiredContacts);

        CheckWinCondition();
        CheckStoryProgression();
    }

    public void EndCurrentDay()
    {
        if (gameEnded)
        {
            return;
        }

        Debug.Log("Tag " + currentDay + " beendet.");

        currentDay++;

        if (currentDay > finalDay)
        {
            CheckLoseCondition();
            return;
        }

        Debug.Log("Neuer Tag gestartet: " + currentDay + " / " + finalDay);
        CheckStoryProgression();
    }

    private void CheckWinCondition()
    {
        if (gameEnded)
        {
            return;
        }

        if (ConfirmedContacts >= requiredContacts)
        {
            TriggerWin();
        }
    }

    private void CheckLoseCondition()
    {
        if (gameEnded)
        {
            return;
        }

        if (currentDay > finalDay && ConfirmedContacts < requiredContacts)
        {
            TriggerLose();
        }
    }

    private void TriggerWin()
    {
        gameEnded = true;
        SetState(GameState.EndingWin);

        Debug.Log("WIN: 20 bestätigte Kontakte wurden rechtzeitig dokumentiert.");
    }

    private void TriggerLose()
    {
        gameEnded = true;
        SetState(GameState.EndingLose);

        Debug.Log("LOSE: Tag 15 ist vorbei und es wurden nicht genug Kontakte dokumentiert.");
    }
    private void CheckStoryProgression()
    {
        if (StoryEventManager.Instance == null)
        {
            return;
        }

        StoryEventManager.Instance.CheckStoryEvents(currentDay, ConfirmedContacts);
}
}