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

    public void Activate()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        rend.material.color = Color.green;

        fadeCoroutine = StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float duration = 1f;
        float elapsed = 0f;

        Color startColor = Color.green;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            rend.material.color = Color.Lerp(
                startColor,
                originalColor,
                elapsed / duration
            );

            yield return null;
        }

        rend.material.color = originalColor;
    }
}