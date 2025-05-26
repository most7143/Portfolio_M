using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMemoryPopup : UIPopup
{
    public List<UIMemorySlot> Slots;
    public TextMeshProUGUI MemoryPointText;
    public XButton ResetButton;

    public void Awake()
    {
        ResetButton.OnExecute = ClickReset;
        ResetButton.IsPressClick = false;
    }

    private void OnEnable()
    {
        EventManager<EventTypes>.Register(EventTypes.RefreshMemory, Refresh);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister(EventTypes.RefreshMemory, Refresh);
    }

    public override void Spawn()
    {
        base.Spawn();

        PlayerPrefs.SetInt("MemoryPoint", PlayerPrefs.GetInt("MemoryMaxPoint"));

        MemoryPointText.SetText(string.Format("기억 포인트 {0} / {1}", PlayerPrefs.GetInt("MemoryPoint"), PlayerPrefs.GetInt("MemoryMaxPoint")));

        for (int i = 0; i < Slots.Count; i++)
        {
            Slots[i].Spawn();
        }

        Refresh();
    }

    public void ClickReset()
    {
        if (PlayerPrefs.GetInt("MemoryPoint") == PlayerPrefs.GetInt("MemoryMaxPoint"))
            return;

        for (int i = 0; i < Slots.Count; i++)
        {
            string key = "Memory" + Slots[i].Name.ToString();
            PlayerPrefs.DeleteKey(key);
            Slots[i].RefreshPoint();
        }

        PlayerPrefs.SetInt("MemoryPoint", PlayerPrefs.GetInt("MemoryMaxPoint"));
        EventManager<EventTypes>.Send(EventTypes.RefreshMemory);
        Refresh();
    }

    public void Refresh()
    {
        MemoryPointText.SetText(string.Format("기억 포인트 {0} / {1}", PlayerPrefs.GetInt("MemoryPoint"), PlayerPrefs.GetInt("MemoryMaxPoint")));
    }
}