using UnityEngine;

public class LogbookUIController : MonoBehaviour
{
    [SerializeField] private GameObject logbookPanel;

    public void OpenLogbook()
    {
        logbookPanel.SetActive(true);
    }

    public void CloseLogbook()
    {
        logbookPanel.SetActive(false);
    }
}