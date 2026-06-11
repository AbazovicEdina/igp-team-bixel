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
        Instance = this;

        // Test-Sequenzen
        responses.Add("1-2-3", ResponseType.Identical);
        responses.Add("3-1-6", ResponseType.Identical);
        responses.Add("2-4-8", ResponseType.Identical);

        responses.Add("1-1-5", ResponseType.Distorted);
        responses.Add("7-3-2", ResponseType.Distorted);
        responses.Add("0-1-2", ResponseType.Identical);
    }

    public ResponseType GetResponse(List<int> sequence)
    {
        string key = string.Join("-", sequence);

        if (responses.ContainsKey(key))
        {
            return responses[key];
        }

        return ResponseType.None;
    }
}