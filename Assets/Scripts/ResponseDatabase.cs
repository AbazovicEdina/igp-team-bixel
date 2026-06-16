using System.Collections.Generic;
using UnityEngine;

public enum ResponseType
{
    None,
    Identical,
    Distorted
}

public class ResponseDatabase : MonoBehaviour
{
    public static ResponseDatabase Instance;

    private Dictionary<string, ResponseType> responses = new Dictionary<string, ResponseType>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        RegisterConfirmedSequences();
        RegisterDistortedSequences();
    }

    private void RegisterConfirmedSequences()
    {
        // 20 bestätigte Kontakt-Sequenzen
        // IDs gehen aktuell von 0 bis 7, weil der SoundboardManager Tastatur 1-8 intern als 0-7 speichert.

        Register("0-1-2", ResponseType.Identical);
        Register("1-3-5", ResponseType.Identical);
        Register("2-4-6", ResponseType.Identical);
        Register("3-5-7", ResponseType.Identical);
        Register("0-2-4", ResponseType.Identical);

        Register("1-4-7", ResponseType.Identical);
        Register("2-5-0", ResponseType.Identical);
        Register("3-6-1", ResponseType.Identical);
        Register("4-7-2", ResponseType.Identical);
        Register("5-0-3", ResponseType.Identical);

        Register("0-3-6-1", ResponseType.Identical);
        Register("1-4-7-2", ResponseType.Identical);
        Register("2-5-0-3", ResponseType.Identical);
        Register("3-6-1-4", ResponseType.Identical);
        Register("4-7-2-5", ResponseType.Identical);

        Register("0-2-5-7", ResponseType.Identical);
        Register("1-3-6-0", ResponseType.Identical);
        Register("2-4-7-1", ResponseType.Identical);
        Register("3-5-0-2", ResponseType.Identical);
        Register("4-6-1-3", ResponseType.Identical);
    }

    private void RegisterDistortedSequences()
    {
        // Verzerrte Signale: zählen nicht als bestätigter Kontakt,
        // können aber später an HQ gemeldet werden.

        Register("0-0-1", ResponseType.Distorted);
        Register("1-1-2", ResponseType.Distorted);
        Register("2-2-3", ResponseType.Distorted);
        Register("3-3-4", ResponseType.Distorted);
        Register("4-4-5", ResponseType.Distorted);

        Register("7-6-5", ResponseType.Distorted);
        Register("6-4-2", ResponseType.Distorted);
        Register("5-3-1", ResponseType.Distorted);
        Register("2-0-7", ResponseType.Distorted);
        Register("7-0-2-4", ResponseType.Distorted);
    }

    private void Register(string sequenceKey, ResponseType responseType)
    {
        if (responses.ContainsKey(sequenceKey))
        {
            Debug.LogWarning("ResponseDatabase: Sequenz bereits vorhanden: " + sequenceKey);
            return;
        }

        responses.Add(sequenceKey, responseType);
    }

    public ResponseType GetResponse(List<int> sequence)
    {
        string key = ConvertSequenceToKey(sequence);

        if (responses.ContainsKey(key))
        {
            return responses[key];
        }

        return ResponseType.None;
    }

    public bool IsKnownSequence(List<int> sequence)
    {
        string key = ConvertSequenceToKey(sequence);
        return responses.ContainsKey(key);
    }

    public int GetKnownResponseCount()
    {
        return responses.Count;
    }

    private string ConvertSequenceToKey(List<int> sequence)
    {
        return string.Join("-", sequence);
    }
}