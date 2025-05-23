using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMemorySlot : MonoBehaviour
{
    public StatNames Name;
    public string NameString;
    public int Count;
    public Image Icon;
    public List<Image> PointImages;
    public List<float> Values;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI ValueText;

    public XButton Button;

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
        Refresh();
    }

    public void Refresh()
    {
        NameText.SetText(EXText.GetStatLanguage(Name));
        Icon.sprite = ResourcesManager.Instance.LoadSprite("Icon_Memory_" + Name.ToString());
        RefreshPoint();
    }

    public void Click()
    {
        if (Count < 4)
        {
            Count++;

            OutGameData.Instance.AddOutGameData("Momory" + Name.ToString(), 1);

            RefreshPoint();
        }
    }

    public void RefreshPoint()
    {
        Count = PlayerPrefs.GetInt("Momory" + Name.ToString());

        for (int i = 0; i < PointImages.Count; i++)
        {
            if (Count > i)
            {
                PointImages[i].sprite = ResourcesManager.Instance.LoadSprite("UI_Memory_Activate");
            }
            else
            {
                PointImages[i].sprite = ResourcesManager.Instance.LoadSprite("UI_Memory_Deactivate");
            }
        }

        if (Count > 0)
        {
            ValueText.SetText(EXText.GetStatPercent(Name, Values[Count - 1]));
        }
        else
        {
            ValueText.SetText("");
        }
    }
}