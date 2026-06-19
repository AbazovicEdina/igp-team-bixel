using System.Collections.Generic;
using UnityEngine;

public class TransmissionController : MonoBehaviour
{
    public static TransmissionController Instance;

    [SerializeField] private KeyCode clearKey = KeyCode.Backspace;
    [SerializeField] private KeyCode transmitKey = KeyCode.Return;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

   private void Update()
{
    if (Input.GetKeyDown(transmitKey))
    {
        SubmitTransmission();
    }

    if (Input.GetKeyDown(clearKey))
    {
        ClearCurrentSequence();
    }
}

    public void SubmitTransmission()
    {
        if (GameManager.Instance != null &&
            !GameManager.Instance.CanAcceptGameplayInput())
        {
            return;
        }

        if (SequenceBuilder.Instance == null)
        {
            Debug.LogError("SequenceBuilder fehlt.");
            return;
        }

        if (ResponseDatabase.Instance == null)
        {
            Debug.LogError("ResponseDatabase fehlt.");
            return;
        }

        List<int> sequence =
            new List<int>(SequenceBuilder.Instance.currentSequence);

        if (sequence.Count != 3)
        {
            Debug.LogWarning("Eine Sequenz muss genau 3 Runen enthalten.");
            return;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetState(GameState.Transmitting);
        }

        bool success =
            ResponseDatabase.Instance.CheckCurrentContact(sequence);

        if (success)
        {
            string successMessage =
                "CONTACT ESTABLISHED\n\n" +
                "CONTACT " +
                ResponseDatabase.Instance.GetCurrentContactIndex() +
                " / " +
                ResponseDatabase.Instance.GetTotalContacts();

            ReceiveDisplay.Instance?.ShowMessage(successMessage);

            LogbookManager.Instance?.AddEntry(
                sequence,
                "CONTACT ESTABLISHED"
            );

            if (TutorialManager.Instance != null)
            {
                TutorialManager.Instance.NotifyContactConfirmed();
            }

            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnConfirmedContactAdded();
            }

            ResponseDatabase.Instance.AdvanceContact();
        }
       else
{
    SignalAnalysis analysis =
        ResponseDatabase.Instance.AnalyzeSignal(sequence);

    SequenceBuilder.Instance.ShowFeedback(analysis);

    string hint =
        "SIGNAL ANALYSIS\n\n" +
        "TONES MATCHED: " + analysis.correctRunes + "/3\n" +
        "POSITIONS VERIFIED: " + analysis.correctPositions + "/3";

    ReceiveDisplay.Instance?.ShowMessage(hint);

    LogbookManager.Instance?.AddEntry(
        sequence,
        hint
    );
}
        GameManager.Instance?.OnTransmissionSent();
        SequenceBuilder.Instance.Clear();

        if (GameManager.Instance != null &&
            !GameManager.Instance.IsGameEnded)
        {
            GameManager.Instance.SetState(GameState.Playing);
        }
    }

    public void ClearCurrentSequence()
    {
        if (SequenceBuilder.Instance == null)
        {
            return;
        }

        SequenceBuilder.Instance.Clear();

        Debug.Log("Aktuelle Sequenz gelöscht.");
    }
    
}