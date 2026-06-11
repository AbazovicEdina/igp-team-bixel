using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LogbookEntry
{
    public string sequenceKey;
    public List<int> sequence;
    public ResponseType responseType;
    public string note;

    public LogbookEntry(List<int> sequence, ResponseType responseType, string note)
    {
        this.sequence = new List<int>(sequence);
        this.sequenceKey = string.Join("-", sequence);
        this.responseType = responseType;
        this.note = note;
    }
}

public class LogbookManager : MonoBehaviour
{
    public static LogbookManager Instance;

    [SerializeField]
    private List<LogbookEntry> confirmedContacts = new List<LogbookEntry>();

    [SerializeField]
    private List<LogbookEntry> reportedSignals = new List<LogbookEntry>();

    private HashSet<string> confirmedSequenceKeys = new HashSet<string>();

    public int ConfirmedContactCount
    {
        get { return confirmedContacts.Count; }
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

    public bool TryAddConfirmedSequence(List<int> sequence)
    {
        if (sequence == null || sequence.Count == 0)
        {
            Debug.LogWarning("Logbook: Leere Sequenz kann nicht gespeichert werden.");
            return false;
        }

        string key = ConvertSequenceToKey(sequence);

        if (confirmedSequenceKeys.Contains(key))
        {
            Debug.Log("Logbook: Sequenz wurde bereits gespeichert: " + key);
            return false;
        }

        LogbookEntry newEntry = new LogbookEntry(
            sequence,
            ResponseType.Identical,
            "Confirmed communication contact."
        );

        confirmedContacts.Add(newEntry);
        confirmedSequenceKeys.Add(key);

        Debug.Log("Logbook: Neuer Kontakt gespeichert: " + key);
        Debug.Log("Bestätigte Kontakte: " + ConfirmedContactCount);

        return true;
    }

    public void AddReportedSignal(List<int> sequence, ResponseType responseType)
    {
        if (sequence == null || sequence.Count == 0)
        {
            Debug.LogWarning("Logbook: Leere Sequenz kann nicht gemeldet werden.");
            return;
        }

        LogbookEntry newEntry = new LogbookEntry(
            sequence,
            responseType,
            "Signal reported to headquarters."
        );

        reportedSignals.Add(newEntry);

        Debug.Log("Logbook: Signal an Zentrale gemeldet: " + ConvertSequenceToKey(sequence));
    }

    public bool HasConfirmedSequence(List<int> sequence)
    {
        string key = ConvertSequenceToKey(sequence);
        return confirmedSequenceKeys.Contains(key);
    }

    public List<LogbookEntry> GetConfirmedContacts()
    {
        return confirmedContacts;
    }

    public List<LogbookEntry> GetReportedSignals()
    {
        return reportedSignals;
    }

    public void ClearLogbook()
    {
        confirmedContacts.Clear();
        reportedSignals.Clear();
        confirmedSequenceKeys.Clear();

        Debug.Log("Logbook wurde geleert.");
    }

    private string ConvertSequenceToKey(List<int> sequence)
    {
        return string.Join("-", sequence);
    }
}