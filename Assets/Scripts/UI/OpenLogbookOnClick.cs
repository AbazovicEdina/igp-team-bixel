using UnityEngine;

public class OpenLogbookOnClick : MonoBehaviour
{
    [SerializeField] private GameObject logbookPanel;

    private void OnMouseDown()
    {
        logbookPanel.SetActive(true);
    }
}