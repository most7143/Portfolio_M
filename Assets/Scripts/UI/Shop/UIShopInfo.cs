using System;
using System.Collections.Generic;
using UnityEngine;

public class UIShopInfo : MonoBehaviour
{
    public RectTransform Content;
    private List<UIPocketBox> Pockets = new();

    private void OnEnable()
    {
        EventManager<EventTypes>.Register(EventTypes.AddCurrency, Refresh);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister(EventTypes.AddCurrency, Refresh);
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        PocketTypes[] types = (PocketTypes[])Enum.GetValues(typeof(PocketTypes));

        for (int i = 1; i < types.Length; i++)
        {
            UIPocketBox box = ResourcesManager.Instance.Load<UIPocketBox>("UIPocketBox");

            if (box != null)
            {
                box.Init(types[i]);
                box.transform.SetParent(Content);
                Pockets.Add(box);
            }
        }
    }

    public void Activate()
    {
        for (int i = 0; i < Pockets.Count; i++)
        {
            Pockets[i].Actiavte();
        }
    }

    public void Refresh()
    {
        for (int i = 0; i < Pockets.Count; i++)
        {
            Pockets[i].Actiavte();
        }
    }
}