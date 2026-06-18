using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        int day = GameManager.Instance.CurrentDay;

        float shakeAmount = 0f;

        if (day >= 15)
            shakeAmount = 0.2f;
        else if (day >= 14)
            shakeAmount = 0.08f;
            else if (day >= 13)
            shakeAmount = 0.05f;
            else if (day >= 12)
            shakeAmount = 0.015f;
            else if (day >= 10)
            shakeAmount = 0.01f;
            

        if (shakeAmount > 0f)
        {
            transform.localPosition =
                originalPosition +
                Random.insideUnitSphere * shakeAmount;
        }
        else
        {
            transform.localPosition = originalPosition;
        }
    }
}