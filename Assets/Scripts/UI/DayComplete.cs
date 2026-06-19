using TMPro;
using UnityEngine;

public class DayCompleteUI : MonoBehaviour
{
    [SerializeField] private TMP_Text contactsText;
    [SerializeField] private TMP_Text daysRemainingText;

    public void Refresh()
    {
        contactsText.text =
            "CONFIRMED CONTACTS: " +
            GameManager.Instance.ConfirmedContacts +
            " / " +
            GameManager.Instance.RequiredContacts;

        int remainingDays =
            GameManager.Instance.FinalDay -
            GameManager.Instance.CurrentDay;

        daysRemainingText.text =
            "DAYS REMAINING: " +
            remainingDays;
    }
}