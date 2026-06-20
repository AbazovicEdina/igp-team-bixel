using TMPro;
using UnityEngine;

public class FinalDayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text contactsText;

    public void Refresh()
    {
        contactsText.text =
            "CONFIRMED CONTACTS: " +
            GameManager.Instance.ConfirmedContacts +
            " / " +
            GameManager.Instance.RequiredContacts;
    }

    public void BeginFinalDay()
    {
        gameObject.SetActive(false);
        GameManager.Instance.SetState(GameState.Playing);
    }
}