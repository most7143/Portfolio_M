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
        EventManager<EventTypes>.Register<CurrencyTypes>(EventTypes.UseCurrency, Refresh);
        EventManager<EventTypes>.Register(EventTypes.RefreshAccessory, RefreshAcc);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister<CurrencyTypes>(EventTypes.AddCurrency, Refresh);
        EventManager<EventTypes>.Unregister<CurrencyTypes>(EventTypes.UseCurrency, Refresh);
        EventManager<EventTypes>.Unregister(EventTypes.RefreshAccessory, RefreshAcc);
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        PocketTypes[] types = (PocketTypes[])Enum.GetValues(typeof(PocketTypes));

        for (int i = types.Length - 1; i > 0; i--)
        {
            UIPocketBox box = ResourcesManager.Instance.Load<UIPocketBox>("UIPocketBox");

            if (box != null)
            {
                box.transform.SetParent(Content);
                box.transform.SetAsFirstSibling();
                box.Init(types[i]);
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