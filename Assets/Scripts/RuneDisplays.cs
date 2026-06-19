using System.Collections;
using UnityEngine;

public class RuneDisplay : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;

    private Coroutine fadeCoroutine;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    public void Flash()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        Color startColor = rend.material.color;

        rend.material.color = Color.white;

        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            rend.material.color = Color.Lerp(
                Color.white,
                startColor,
                elapsed / duration
            );

            yield return null;
        }

        rend.material.color = startColor;
    }

    public void SetCorrectPosition()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        rend.material.color = Color.green;
    }

    public void SetCorrectRune()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        rend.material.color = Color.blue;
    }

    public void ResetRune()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        rend.material.color = originalColor;
    }
}