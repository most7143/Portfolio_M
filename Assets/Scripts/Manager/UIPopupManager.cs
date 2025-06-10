using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public UIPopup PausePopup;

    private Dictionary<UIPopupNames, UIPopup> popups = new();

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "TitleScene")
            {
                if (Popup != null)
                {
                    Despawn();
                }
            }
            else if (SceneManager.GetActiveScene().name == "MainScene")
            {
                if (IsPausePopup())
                {
                    DespawnPausePopup();
                }
                else
                {
                    SpawnPausePopup();
                }
            }
        }
    }

    private bool IsPausePopup()
    {
        if (PausePopup == null)
        {
            return false;
        }

        if (false == PausePopup.gameObject.activeInHierarchy)
        {
            return false;
        }

        return true;
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

    public void SpawnPausePopup()
    {
        if (PausePopup == null)
        {
            GameObject obj = ResourcesManager.Instance.Load(UIPopupNames.GamePause);

            if (obj != null)
            {
                PausePopup = obj.GetComponent<UIPopup>();
                PausePopup.gameObject.transform.SetParent(transform, false);
            }
        }
        PausePopup.transform.SetAsLastSibling();
        PausePopup.gameObject.SetActive(true);
        PausePopup.Spawn();
    }

    public void DespawnPausePopup()
    {
        PausePopup.Despawn();
        PausePopup.gameObject.SetActive(false);
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