using System.Collections.Generic;
using UnityEngine;

public class TransmissionController : MonoBehaviour
{
    public static TransmissionController Instance;

    [SerializeField] private KeyCode transmitKey = KeyCode.Return;
    [SerializeField] private KeyCode clearKey = KeyCode.Backspace;

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
        // Nur zum Testen:
        // Enter = Sequenz absenden
        // Backspace = Sequenz löschen

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
        if (GameManager.Instance != null && !GameManager.Instance.CanAcceptGameplayInput())
        {
            Debug.Log("Transmission nicht möglich. Aktueller State: " + GameManager.Instance.CurrentState);
            return;
        }

        if (SequenceBuilder.Instance == null)
        {
            Debug.LogError("Transmission fehlgeschlagen: SequenceBuilder fehlt in der Szene.");
            return;
        }

        if (ResponseDatabase.Instance == null)
        {
            Debug.LogError("Transmission fehlgeschlagen: ResponseDatabase fehlt in der Szene.");
            return;
        }

        List<int> sequence = new List<int>(SequenceBuilder.Instance.currentSequence);

        if (sequence.Count == 0)
        {
            Debug.LogWarning("Transmission fehlgeschlagen: Keine Sequenz eingegeben.");
            return;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetState(GameState.Transmitting);
        }

        Debug.Log("Transmission gesendet: " + ConvertSequenceToKey(sequence));

        ResponseType response = ResponseDatabase.Instance.GetResponse(sequence);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetState(GameState.Receiving);
        }

        HandleResponse(sequence, response);

        SequenceBuilder.Instance.Clear();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetState(GameState.Playing);
        }
    }

    private void HandleResponse(List<int> sequence, ResponseType responseType)
    {
        string sequenceKey = ConvertSequenceToKey(sequence);

        switch (responseType)
        {
            case ResponseType.None:
                Debug.Log("Keine Antwort für Sequenz: " + sequenceKey);
                break;

            case ResponseType.Identical:
                Debug.Log("Identische Antwort erhalten. Kontakt bestätigt: " + sequenceKey);

                if (LogbookManager.Instance != null)
                {
                    bool wasAdded = LogbookManager.Instance.TryAddConfirmedSequence(sequence);

                    if (wasAdded)
                        {
                            Debug.Log("Kontaktzahl: " + LogbookManager.Instance.ConfirmedContactCount);

                            if (GameManager.Instance != null)
                            {
                                GameManager.Instance.OnConfirmedContactAdded();
                            }
                        }
                }
                else
                {
                    Debug.LogWarning("LogbookManager fehlt in der Szene. Kontakt wurde nicht gespeichert.");
                }

                break;

            case ResponseType.Distorted:
                Debug.Log("Verzerrte Antwort erhalten: " + sequenceKey);

                if (LogbookManager.Instance != null)
                {
                    LogbookManager.Instance.AddReportedSignal(sequence, responseType);
                }
                else
                {
                    Debug.LogWarning("LogbookManager fehlt in der Szene. Verzerrtes Signal wurde nicht gemeldet.");
                }

                break;
        }
    }

    public void ClearCurrentSequence()
    {
        if (SequenceBuilder.Instance == null)
        {
            Debug.LogError("Sequenz kann nicht gelöscht werden: SequenceBuilder fehlt.");
            return;
        }

        SequenceBuilder.Instance.Clear();
        Debug.Log("Aktuelle Sequenz gelöscht.");
    }

    private string ConvertSequenceToKey(List<int> sequence)
    {
        return string.Join("-", sequence);
    }
}