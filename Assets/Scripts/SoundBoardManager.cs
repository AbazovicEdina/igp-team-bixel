using UnityEngine;

public class SoundboardManager : MonoBehaviour
{
    public static SoundboardManager Instance;

    private KeyCode[] buttonKeys =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8
    };

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!GameManager.Instance.CanAcceptGameplayInput())
            return;

        for (int i = 0; i < 8; i++)
        {
            if (Input.GetKeyDown(buttonKeys[i]))
            {
                OnButtonPressed(i+1);
            }
        }
    }

    public void OnButtonPressed(int id)
    {
        Debug.Log("Button gedrückt: " + id);

        SequenceBuilder.Instance.AddSymbol(id);
    }
}