using System.Collections;
using UnityEngine;

public class UIPopup : MonoBehaviour
{
    public CanvasGroup CanvasGroup;
    public RectTransform Rect;
    public bool IsStopBattle = true;
    public UIPopupNames Name;
    public UIFade Fade;

    public virtual void Spawn()
    {
        if (IsStopBattle)
        {
            InGameManager.Instance.PauseBattle();
        }

        if (Fade != null)
        {
            Fade.FadeIn();
        }
    }

    public virtual void Despawn()
    {
        if (IsStopBattle)
        {
            InGameManager.Instance.ContinueBattle();
        }
    }

    public void SpawnToWaitInteract(float time)
    {
        StopAllCoroutines();
        StartCoroutine(WaitInteract(time));
    }

    private IEnumerator WaitInteract(float time)
    {
        CanvasGroup.blocksRaycasts = false;
        yield return new WaitForSecondsRealtime(time);
        CanvasGroup.blocksRaycasts = true;
    }
}