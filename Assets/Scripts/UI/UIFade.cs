using System.Collections;
using UnityEngine;

public class UIFade : MonoBehaviour
{
    public Canvas Canvas;
    public CanvasGroup Group;
    public float FadeDelay = 0;
    public float FadeDuration = 1f;

    private void OnEnable()
    {
        if (Canvas != null)
        {
            Canvas.worldCamera = Camera.main;
        }
    }

    public void FadeIn()
    {
        StopAllCoroutines();
        Group.alpha = 0;
        StartCoroutine(FadeCanvasGroup(Group, Group.alpha, 1));
    }

    public void FadeOut()
    {
        StopAllCoroutines();
        Group.alpha = 1;
        StartCoroutine(FadeCanvasGroup(Group, Group.alpha, 0));
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float startAlpha, float endAlpha)
    {
        yield return new WaitForSeconds(FadeDelay);

        float elapsedTime = 0f;

        while (elapsedTime < FadeDuration)
        {
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / FadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cg.alpha = endAlpha;
    }
}