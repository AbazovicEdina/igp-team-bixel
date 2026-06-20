using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LogbookEntry
{
    public string sequenceKey;
    public List<int> sequence;
    public string result;
    public int day;

    public LogbookEntry(
        List<int> sequence,
        string result,
        int day)
    {
        this.sequence = new List<int>(sequence);
        this.sequenceKey = string.Join("-", sequence);
        this.result = result;
        this.day = day;
    }
}

public class LogbookManager : MonoBehaviour
{
    public static LogbookManager Instance;

    [Header("Logbook Entries")]
    [SerializeField]
    private List<LogbookEntry> entries = new();

    public event Action<LogbookEntry> OnEntryAdded;

    public int EntryCount
    {
        get { return entries.Count; }
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

    public void AddEntry(
        List<int> sequence,
        string result)
    {
        if (sequence == null || sequence.Count == 0)
        {
            return;
        }

        LogbookEntry entry =
            new LogbookEntry(
                sequence,
                result,
                GetCurrentDay());

        entries.Add(entry);

        Debug.Log(
            "Logbook: " +
            entry.sequenceKey +
            " -> " +
            result);

        OnEntryAdded?.Invoke(entry);
    }

    public List<LogbookEntry> GetEntries()
    {
        return new List<LogbookEntry>(entries);
    }

    public void ClearLogbook()
    {
        entries.Clear();

        Debug.Log("Logbook geleert.");
    }

    private int GetCurrentDay()
    {
        if (GameManager.Instance == null)
        {
            return 1;
        }

        return GameManager.Instance.CurrentDay;
    }
}