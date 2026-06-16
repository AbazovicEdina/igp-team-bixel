using System.Collections.Generic;
using UnityEngine;

public class SequenceBuilder : MonoBehaviour
{
    public static SequenceBuilder Instance;

    public List<int> currentSequence = new();

    private void Awake()
    {
        Instance = this;
    }

    public void AddSymbol(int id)
    {
        currentSequence.Add(id);

        Debug.Log(
            "Sequenz: " +
            string.Join(",", currentSequence)
        );

    if (TutorialManager.Instance != null)
        {
            TutorialManager.Instance.NotifySymbolPressed();
        }     
    }

    public void Clear()
    {
        currentSequence.Clear();
    }
}