using System.Collections.Generic;
using UnityEngine;

public class SequenceBuilder : MonoBehaviour
{
    public static SequenceBuilder Instance;

    [SerializeField]
    private RuneDisplay[] runeDisplays;

    public List<int> currentSequence = new();

    private void Awake()
    {
        Instance = this;
    }

    public void AddSymbol(int id)
    {
        // Beim ersten Tastendruck eines neuen Versuchs
        // altes Feedback entfernen
        if (currentSequence.Count == 0)
        {
            ResetAllRunes();
        }

        if (currentSequence.Count >= 3)
        {
            Debug.Log("Maximal 3 Runen erlaubt.");
            return;
        }

        currentSequence.Add(id);

        if (id >= 1 && id <= runeDisplays.Length)
        {
            runeDisplays[id - 1].Flash();
        }

        Debug.Log(
            "Sequenz: " +
            string.Join(",", currentSequence)
        );

        if (TutorialManager.Instance != null)
        {
            TutorialManager.Instance.NotifySymbolPressed();
        }
    }

    public void ShowFeedback(SignalAnalysis analysis)
{
    Debug.Log("ShowFeedback aufgerufen");

    for (int i = 0; i < currentSequence.Count; i++)
    {
        int runeId = currentSequence[i];

        Debug.Log(
            "Rune " + runeId +
            " Position=" + analysis.correctPosition[i] +
            " Tone=" + analysis.correctRune[i]);

        if (analysis.correctPosition[i])
        {
            runeDisplays[runeId - 1].SetCorrectPosition();
        }
        else if (analysis.correctRune[i])
        {
            runeDisplays[runeId - 1].SetCorrectRune();
        }
    }
}

    public void ResetAllRunes()
    {
        foreach (RuneDisplay rune in runeDisplays)
        {
            rune.ResetRune();
        }
    }

    public void Clear()
    {
        currentSequence.Clear();
    }
}