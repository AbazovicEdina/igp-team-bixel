using System.Collections.Generic;
using UnityEngine;

public class ResponseDatabase : MonoBehaviour
{
    public static ResponseDatabase Instance;

    private int currentContact = 0;

    private List<List<int>> contacts = new()
    {
        new() { 1, 2, 3 },
        new() { 4, 6, 2 },
        new() { 3, 5, 7 },
        new() { 8, 1, 5 },
        new() { 2, 7, 4 }
    };

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public List<int> GetCurrentTarget()
    {
        return contacts[currentContact];
    }

    public bool CheckCurrentContact(List<int> input)
    {
        List<int> target = GetCurrentTarget();

        for (int i = 0; i < 3; i++)
        {
            if (input[i] != target[i])
            {
                return false;
            }
        }

        return true;
    }

    public void AdvanceContact()
    {
        if (currentContact < contacts.Count - 1)
        {
            currentContact++;
        }
    }

    public int GetCurrentContactIndex()
    {
        return currentContact + 1;
    }

    public int GetTotalContacts()
    {
        return contacts.Count;
    }

    public string GetSignalHint(List<int> input)
    {
        List<int> target = GetCurrentTarget();

        int correctRunes = 0;
        int correctPositions = 0;

        for (int i = 0; i < 3; i++)
        {
            if (input[i] == target[i])
            {
                correctPositions++;
            }
        }

        List<int> remainingTarget = new(target);

        foreach (int rune in input)
        {
            if (remainingTarget.Contains(rune))
            {
                correctRunes++;

                remainingTarget.Remove(rune);
            }
        }

        return
            "SIGNAL ANALYSIS\n\n" +
            "TONES MATCHED: " + correctRunes + "/3\n" +
            "POSITIONS VERIFIED: " + correctPositions + "/3";
    }
}