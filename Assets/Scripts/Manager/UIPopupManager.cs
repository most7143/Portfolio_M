using System.Collections.Generic;
using UnityEngine;

public class UIPopupManager : MonoBehaviour
{
    private static UIPopupManager instance = null;

    public UIPopupNames Name
    {
        get
        {
            if (Popup != null)
            {
                return Popup.Name;
            }

            return UIPopupNames.None;
        }
    }

    public UIPopup Popup;

    private Dictionary<UIPopupNames, UIPopup> popups = new();

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static UIPopupManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void Spawn(UIPopupNames popupName, bool spawnData = false)
    {
        if (Popup != null)
            Despawn();

        UIPopup popup;

        if (popups.ContainsKey(popupName))
        {
            popup = popups[popupName];
        }
        else
        {
            GameObject obj = ResourcesManager.Instance.Load(popupName);

            if (obj != null)
            {
                popup = obj.GetComponent<UIPopup>();
                popups.Add(popupName, popup);

                if (spawnData)
                {
                    popup.CanvasGroup.alpha = 0f;
                }
            }
        }

        popups[popupName].gameObject.transform.SetParent(transform, false);
        Popup = popups[popupName];
        popups[popupName].gameObject.SetActive(true);
        popups[popupName].Spawn();
    }

    public void Despawn()
    {
        if (Popup != null)
        {
            Popup.Despawn();
            Popup.CanvasGroup.alpha = 1f;
            Popup.gameObject.SetActive(false);
            Popup = null;
        }
    }
}