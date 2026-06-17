using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LogbookEntry
{
    public string sequenceKey;
    public List<int> sequence;
    public ResponseType responseType;
    public string note;
    public int day;

    public LogbookEntry(List<int> sequence, ResponseType responseType, string note, int day)
    {
        this.sequence = new List<int>(sequence);
        this.sequenceKey = string.Join("-", sequence);
        this.responseType = responseType;
        this.note = note;
        this.day = day;
    }
}

public class LogbookManager : MonoBehaviour
{
    public static LogbookManager Instance;

    [Header("Stored Logbook Data")]
    [SerializeField] private List<LogbookEntry> confirmedContacts = new List<LogbookEntry>();
    [SerializeField] private List<LogbookEntry> reportedSignals = new List<LogbookEntry>();

    private HashSet<string> confirmedSequenceKeys = new HashSet<string>();
    private HashSet<string> reportedSignalKeys = new HashSet<string>();

    public event Action<LogbookEntry> OnConfirmedContactAdded;
    public event Action<LogbookEntry> OnReportedSignalAdded;

    public int ConfirmedContactCount
    {
        get { return confirmedContacts.Count; }
    }

    public int ReportedSignalCount
    {
        get { return reportedSignals.Count; }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        RebuildLookupTables();
    }

    public bool TryAddConfirmedSequence(List<int> sequence)
    {
        if (!IsValidSequence(sequence))
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

        int currentDay = GetCurrentDay();

        LogbookEntry newEntry = new LogbookEntry(
            sequence,
            ResponseType.Identical,
            "Confirmed communication contact.",
            currentDay
        );

        confirmedContacts.Add(newEntry);
        confirmedSequenceKeys.Add(key);

        Debug.Log("Logbook: Neuer Kontakt gespeichert: " + key);
        Debug.Log("Bestätigte Kontakte: " + ConfirmedContactCount);

        OnConfirmedContactAdded?.Invoke(newEntry);

        return true;
    }

    public bool AddReportedSignal(List<int> sequence, ResponseType responseType)
    {
        if (!IsValidSequence(sequence))
        {
            Debug.LogWarning("Logbook: Leere Sequenz kann nicht gemeldet werden.");
            return false;
        }

        string key = ConvertSequenceToKey(sequence);

        if (reportedSignalKeys.Contains(key))
        {
            Debug.Log("Logbook: Signal wurde bereits gemeldet: " + key);
            return false;
        }

        int currentDay = GetCurrentDay();

        LogbookEntry newEntry = new LogbookEntry(
            sequence,
            responseType,
            "Signal reported to headquarters.",
            currentDay
        );

        reportedSignals.Add(newEntry);
        reportedSignalKeys.Add(key);

        Debug.Log("Logbook: Signal an Zentrale gemeldet: " + key);

        OnReportedSignalAdded?.Invoke(newEntry);

        return true;
    }

    public bool HasConfirmedSequence(List<int> sequence)
    {
        if (!IsValidSequence(sequence))
        {
            return false;
        }

        string key = ConvertSequenceToKey(sequence);
        return confirmedSequenceKeys.Contains(key);
    }

    public bool HasReportedSignal(List<int> sequence)
    {
        if (!IsValidSequence(sequence))
        {
            return false;
        }

        string key = ConvertSequenceToKey(sequence);
        return reportedSignalKeys.Contains(key);
    }

    public List<LogbookEntry> GetConfirmedContacts()
    {
        return new List<LogbookEntry>(confirmedContacts);
    }

    public List<LogbookEntry> GetReportedSignals()
    {
        return new List<LogbookEntry>(reportedSignals);
    }

    public void ClearLogbook()
    {
        confirmedContacts.Clear();
        reportedSignals.Clear();

        confirmedSequenceKeys.Clear();
        reportedSignalKeys.Clear();

        Debug.Log("Logbook wurde geleert.");
    }

    private void RebuildLookupTables()
    {
        confirmedSequenceKeys.Clear();
        reportedSignalKeys.Clear();

        foreach (LogbookEntry entry in confirmedContacts)
        {
            confirmedSequenceKeys.Add(entry.sequenceKey);
        }

        foreach (LogbookEntry entry in reportedSignals)
        {
            reportedSignalKeys.Add(entry.sequenceKey);
        }
    }

    private bool IsValidSequence(List<int> sequence)
    {
        return sequence != null && sequence.Count > 0;
    }

    private int GetCurrentDay()
    {
        if (GameManager.Instance == null)
        {
            return 1;
        }

        return GameManager.Instance.CurrentDay;
    }

    private string ConvertSequenceToKey(List<int> sequence)
    {
        return string.Join("-", sequence);
    }
}