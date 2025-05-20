using Assets.Scripts.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMonsterInfo : MonoBehaviour
{
    protected Monster TargetMonster;
    public Image HPBar;
    public RectTransform NameTextRect;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI HPText;
    public List<Image> EliteIcons;
    public TextMeshProUGUI EliteDescText;

    public void Refresh(Monster targetMonster)
    {
        TargetMonster = targetMonster;
        SetInfo(TargetMonster);
        RefreshHPBar();

        RefreshEliteIcon(targetMonster);
    }

    private void RefreshEliteIcon(Monster targetMonster)
    {
        if (targetMonster.EliteType != EliteTypes.None)
        {
            for (int i = 0; i < EliteIcons.Count; i++)
            {
                EliteIcons[i].gameObject.SetActive(true);
            }

            EliteDescText.gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < EliteIcons.Count; i++)
            {
                EliteIcons[i].gameObject.SetActive(false);
            }

            EliteDescText.gameObject.SetActive(false);
        }
    }

    public void SetInfo(Monster target)
    {
        TargetMonster = target;

        string nameString = target.NameString;

        if (target.EliteType != EliteTypes.None)
        {
            nameString = EXText.GetEliteLanguage(target.EliteType) + " " + nameString;
            EliteDescText.SetText(EXText.GetEliteDescLanguage(target.EliteType));
        }

        NameText.SetText("Lv." + target.Level + " " + nameString);

        Vector2 sizeDelta = NameTextRect.sizeDelta;
        sizeDelta.x = NameText.preferredWidth;
        NameTextRect.sizeDelta = sizeDelta;

        HPText.SetText(TargetMonster.CurrentHp + " / " + TargetMonster.MaxHP);
    }

    public void RefreshHPBar()
    {
        if (TargetMonster != null)
        {
            UIHandler.UpdateGauge(HPBar, TargetMonster.MaxHP, TargetMonster.CurrentHp, HPText);
        }
    }
}