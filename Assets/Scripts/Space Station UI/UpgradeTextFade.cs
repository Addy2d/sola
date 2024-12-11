using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeTextFade : MonoBehaviour
{
    public float fadeDuration = 2f;        // Duration for the fade effect
    public float targetAlpha = 1f;         // Target transparency level (0 to 1)
    public TextMeshPro uiText;             // For world-space TextMeshPro text

    void Start()
    {
        uiText.alpha = 0f; // Start completely transparent
    }

    public void TriggerFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void TriggerFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        float startAlpha = uiText.alpha;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            uiText.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        uiText.alpha = targetAlpha; // Ensure it reaches targetAlpha
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        float startAlpha = uiText.alpha;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            uiText.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        uiText.alpha = 0f; // Ensure it reaches full transparency
    }

}
