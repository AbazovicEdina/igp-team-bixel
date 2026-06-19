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
    if (currentSequence.Count >= 3)
    {
        Debug.Log("Maximal 3 Runen erlaubt.");
        return;
    }

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