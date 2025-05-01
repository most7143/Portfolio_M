using System.Collections.Generic;
using UnityEngine;

public class UIPopupManager : MonoBehaviour
{
    private static UIPopupManager instance = null;

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

    public void Spawn(UIPopupNames popupName)
    {
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
            }
        }

        popups[popupName].gameObject.transform.SetParent(transform, false);
        Popup = popups[popupName];
        popups[popupName].gameObject.SetActive(true);
        popups[popupName].Spawn();
    }

    public void Despawn()
    {
        Popup.Despawn();
        Popup.gameObject.SetActive(false);
    }
}