using TMPro;
using UnityEngine;

public class ReceiveDisplay : MonoBehaviour
{
    public static ReceiveDisplay Instance;

    [SerializeField] private TMP_Text displayText;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowMessage(string message)
    {
        displayText.text = message;
    }
}