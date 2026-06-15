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
        currentSequence.Add(id);

        Debug.Log(
            "Sequenz: " +
            string.Join(",", currentSequence)
        );

        if (id >= 0 && id < runeDisplays.Length)
        {
            runeDisplays[id].Activate();
        }
    }

    public void Clear()
    {
        currentSequence.Clear();
    }
}