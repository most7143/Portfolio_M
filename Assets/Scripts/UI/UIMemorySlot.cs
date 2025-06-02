using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMemorySlot : MonoBehaviour
{
    public StatNames Name;
    public string NameString;
    public int UsePoint = 1;
    public Image Icon;
    public List<Image> PointImages;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI ValueText;

    public XButton Button;

    private MemoryData memoryData;

    private void OnValidate()
    {
        if (false == string.IsNullOrEmpty(NameString))
        {
            Name = EXEnum.Parse<StatNames>(NameString);
        }
    }

    private void Start()
    {
        Button.OnExecute = Click;
    }

    public void Spawn()
    {
        NameText.SetText(EXText.GetStatLanguage(Name));
        Icon.sprite = ResourcesManager.Instance.LoadSprite("Icon_Memory_" + Name.ToString());

        int point = PlayerPrefs.GetInt("Memory" + Name.ToString());

        RefreshPoint();
    }

    public void Click()
    {
        if (PlayerPrefs.GetInt("MemoryPoint") < UsePoint)
            return;

        OutGameManager.Instance.AddOutGameData("Memory" + Name.ToString(), UsePoint);
        OutGameManager.Instance.AddOutGameData("MemoryPoint", -UsePoint);
        RefreshPoint();

        EventManager<EventTypes>.Send(EventTypes.RefreshMemory);
    }

    public void RefreshPoint()
    {
        int point = PlayerPrefs.GetInt("Memory" + Name.ToString()) / UsePoint;

        for (int i = 0; i < PointImages.Count; i++)
        {
            if (point > i)
            {
                PointImages[i].sprite = ResourcesManager.Instance.LoadSprite("UI_Memory_Activate");
            }
            else
            {
                PointImages[i].sprite = ResourcesManager.Instance.LoadSprite("UI_Memory_Deactivate");
            }
        }

        if (point > 0)
        {
            if (memoryData == null)
            {
                memoryData = ResourcesManager.Instance.LoadScriptable<MemoryData>("Memory_" + Name.ToString());
            }

            ValueText.SetText(EXText.GetStatPercent(Name, memoryData.Values[point - 1]));
        }
        else
        {
            ValueText.SetText("");
        }
    }
}