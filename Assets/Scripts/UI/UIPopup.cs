using UnityEngine;

public class UIPopup : MonoBehaviour
{
    public RectTransform Rect;
    public bool IsStopBattle = true;
    public UIPopupNames Name;

    public virtual void Spawn()
    {
        if (IsStopBattle)
        {
            InGameManager.Instance.PauseBattle();
            EventManager<EventTypes>.Send(EventTypes.RefreshAttackSpeed);
        }
    }

    public virtual void Despawn()
    {
        if (IsStopBattle)
        {
            InGameManager.Instance.ContinueBattle();
            EventManager<EventTypes>.Send(EventTypes.RefreshAttackSpeed);
        }
    }
}