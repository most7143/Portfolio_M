using UnityEngine;

public class UIPopup : MonoBehaviour
{
    public CanvasGroup CanvasGroup;
    public RectTransform Rect;
    public bool IsStopBattle = true;
    public UIPopupNames Name;

    public virtual void Spawn()
    {
        if (IsStopBattle)
        {
            InGameManager.Instance.PauseBattle();
        }
    }

    public virtual void Despawn()
    {
        if (IsStopBattle)
        {
            InGameManager.Instance.ContinueBattle();
        }
    }
}