using UnityEngine;

public class UIPopup : MonoBehaviour
{
    public UIPopupNames Name;

    public virtual void Spawn()
    {
    }

    public virtual void Despawn()
    {
        UIPopupManager.Instance.Despawn();
    }
}