using System.Collections;
using UnityEngine;

public class UIDetails : MonoBehaviour
{
    public CanvasGroup Group;

    public float AliveTime = 3f;
    private float fadeDuration = 0.5f;

    public virtual void Spawn()
    {
        StopAllCoroutines();
        StartCoroutine(StartShow());
    }

    public virtual void Despawn()
    {
    }

    private IEnumerator StartShow()
    {
        Group.blocksRaycasts = true;
        FadeIn();
        yield return new WaitForSeconds(AliveTime);
        FadeOut();
        Group.blocksRaycasts = false;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(Group, Group.alpha, 1));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(Group, Group.alpha, 0));
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cg.alpha = endAlpha;
    }
}