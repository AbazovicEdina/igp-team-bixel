using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogbookEntryUI : MonoBehaviour
{
    [SerializeField] private TMP_Text[] pageTexts;

    private const int entriesPerBookSide = 3;

    private void OnEnable()
    {
        RefreshLogbook();
    }

    public void RefreshLogbook()
    {
        if (LogbookManager.Instance == null)
        {
            ClearAllTexts();

            if (pageTexts.Length > 0 && pageTexts[0] != null)
                pageTexts[0].text = "Logbook missing.";

            return;
        }

        List<LogbookEntry> entries = LogbookManager.Instance.GetEntries();

        for (int i = 0; i < pageTexts.Length; i++)
        {
            int startIndex = i * entriesPerBookSide;

            if (pageTexts[i] != null)
            {
                pageTexts[i].text = BuildText(entries, startIndex);
            }
        }
    }

    private void ClearAllTexts()
    {
        foreach (TMP_Text text in pageTexts)
        {
            if (text != null)
                text.text = "";
        }
    }

    private string BuildText(List<LogbookEntry> entries, int startIndex)
    {
        if (entries.Count <= startIndex)
            return "";

        string text = "";

        int endIndex = Mathf.Min(startIndex + entriesPerBookSide, entries.Count);

        for (int i = startIndex; i < endIndex; i++)
        {
            LogbookEntry e = entries[i];

            text +=
                "#" + (i + 1) + "  D" + e.day + "\n" +
                "Seq: " + e.sequenceKey + "\n" +
                ShortenResult(e.result) + "\n\n";
        }

        return text;
    }

    private string ShortenResult(string result)
    {
        if (string.IsNullOrEmpty(result))
            return "";

        if (result.Contains("CONTACT ESTABLISHED"))
            return "Contact confirmed";

        if (result.Contains("TONES MATCHED"))
            return result
                .Replace("SIGNAL ANALYSIS", "")
                .Replace("TONES MATCHED:", "Tones:")
                .Replace("POSITIONS VERIFIED:", "Pos:")
                .Trim();

        return result;
    }
}