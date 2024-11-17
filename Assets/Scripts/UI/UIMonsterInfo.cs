using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMonsterInfo : MonoBehaviour
{
    protected Monster TargetMonster;
    public Image HPBar;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI HPText;

    public void Refresh(Monster targetMonster)
    {
        TargetMonster = targetMonster;
        SetInfo(TargetMonster);
        RefreshHPBar();
    }

    public void SetInfo(Monster target)
    {
        TargetMonster = target;
        NameText.SetText("Lv." + target.Level + " " + target.NameString);
        HPText.SetText(TargetMonster.CurrentHp + " / " + TargetMonster.MaxHp);
    }

    public void RefreshHPBar()
    {
        if (TargetMonster != null)
        {
            HPBar.fillAmount = (float)TargetMonster.CurrentHp / (float)TargetMonster.MaxHp;
            HPText.SetText(TargetMonster.CurrentHp + " / " + TargetMonster.MaxHp);
        }
    }
}