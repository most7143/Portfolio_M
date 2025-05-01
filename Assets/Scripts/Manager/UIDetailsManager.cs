using System.Collections.Generic;
using UnityEngine;

public class UIDetailsManager : MonoBehaviour
{
    private static UIDetailsManager instance = null;

    public UIDetails Details;

    private Dictionary<UIDetailsNames, UIDetails> _details = new();

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

    public static UIDetailsManager Instance
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

    public void Spawn(UIDetailsNames detailsName)
    {
        UIDetails details;

        if (_details.ContainsKey(detailsName))
        {
            details = _details[detailsName];
        }
        else
        {
            GameObject obj = ResourcesManager.Instance.Load(detailsName);

            if (obj != null)
            {
                details = obj.GetComponent<UIDetails>();
                _details.Add(detailsName, details);
            }
        }

        _details[detailsName].gameObject.transform.SetParent(transform, false);
        Details = _details[detailsName];
        Details.gameObject.SetActive(true);
        Details.Spawn();
    }

    public void Despawn()
    {
        Details.Despawn();
        Details.gameObject.SetActive(false);
    }
}