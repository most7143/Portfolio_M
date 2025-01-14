using System.Collections;
using UnityEngine;

public class UIFade : MonoBehaviour
{
    public CanvasGroup Group;
    public float FadeDelay = 0;
    public float FadeDuration = 1f;

    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadeCanvasGroup(Group, Group.alpha, 1));
    }

    public void FadeOut()
    {
        StopAllCoroutines();
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

        cg.alpha = endAlpha; // 마지막에 정확한 alpha 값 설정
    }
}