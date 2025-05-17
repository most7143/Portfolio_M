using System;
using System.Collections.Generic;
using UnityEngine;

public class UIShopInfo : MonoBehaviour
{
    public RectTransform Content;
    public List<UIAccessoryBox> Accessories;
    private List<UIPocketBox> Pockets = new();

    private void OnEnable()
    {
        EventManager<EventTypes>.Register<CurrencyTypes>(EventTypes.AddCurrency, Refresh);
        EventManager<EventTypes>.Register(EventTypes.RefreshAccessory, RefreshAcc);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister<CurrencyTypes>(EventTypes.AddCurrency, Refresh);
        EventManager<EventTypes>.Unregister(EventTypes.RefreshAccessory, RefreshAcc);
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
        Refresh();

        RefreshAcc();
    }

    public void Refresh(CurrencyTypes type = CurrencyTypes.None)
    {
        for (int i = 0; i < Pockets.Count; i++)
        {
            Pockets[i].Actiavte();
        }
    }

    public void RefreshAcc()
    {
        for (int i = 0; i < Accessories.Count; i++)
        {
            Accessories[i].Refresh();
        }
    }
}