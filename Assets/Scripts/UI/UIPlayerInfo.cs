using Assets.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInfo : MonoBehaviour
{
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI WeaponText;
    public TextMeshProUGUI WeaponDamageText;
    public TextMeshProUGUI AttackSpeedText;
    public TextMeshProUGUI CriticalRateText;
    public TextMeshProUGUI CriticalDamageText;
    public WeaponUpgrade WeaponUpgrade;

    public Image HPbar;
    public TextMeshProUGUI HPText;

    public Image ExpBar;

    public void RefreshGold(int gold)
    {
        GoldText.SetText(gold.ToString());

        WeaponUpgrade.RefreshCostColor();
    }

    public void Setup(Player player)
    {
        RefreshWeaponInfo(player.WeaponController.Info);
        RefreshHp(player);
        RefreshLevel(player.Level);
        RefreshExp();
    }

    public void RefreshWeaponInfo(WeaponInfo weaponInfo)
    {
        WeaponText.SetText("Lv." + weaponInfo.Level + " " + weaponInfo.NameString);
        WeaponDamageText.SetText("공격력 : " + RefreshColorByDamage(weaponInfo.Damage));
        AttackSpeedText.SetText("공격속도 : " + weaponInfo.Speed.ToString());
        CriticalRateText.SetText("치명타 확률 : " + RefreshColorByCriticalRate(weaponInfo.CriticalRate * 100f));
        CriticalDamageText.SetText("치명타 데미지 : " + RefreshColorByCriticalDamage(weaponInfo.CriticalDamage * 100f));
    }

    public string RefreshColorByDamage(float damage)
    {
        if (800 <= damage)
        {
            return "<color=orange>" + damage.ToString() + "</color>";
        }
        else if (400 <= damage)
        {
            return "<color=yellow>" + damage.ToString() + "</color>";
        }
        else if (80 <= damage)
        {
            return "<color=green>" + damage.ToString() + "</color>";
        }
        else
        {
            return "<color=white>" + damage.ToString() + "</color>";
        }
    }

    public string RefreshColorByCriticalRate(float damage)
    {
        if (500f <= damage)
        {
            return "<color=orange>" + damage.ToString() + "%</color>";
        }
        else if (350f <= damage)
        {
            return "<color=yellow>" + damage.ToString() + "%</color>";
        }
        else if (20f <= damage)
        {
            return "<color=green>" + damage.ToString() + "%</color>";
        }
        else
        {
            return "<color=white>" + damage.ToString() + "%</color>";
        }
    }

    public string RefreshColorByCriticalDamage(float damage)
    {
        if (450f <= damage)
        {
            return "<color=orange>" + damage.ToString() + "%</color>";
        }
        else if (350f <= damage)
        {
            return "<color=yellow>" + damage.ToString() + "%</color>";
        }
        else if (250f <= damage)
        {
            return "<color=green>" + damage.ToString() + "%</color>";
        }
        else
        {
            return "<color=white>" + damage.ToString() + "%</color>";
        }
    }

    public void RefreshLevel(int level)
    {
        LevelText.SetText("Rank " + level);
    }

    public void RefreshHp(Player player)
    {
        UIHandler.UpdateGauge(HPbar, player.MaxHp, player.CurrentHp, HPText);
    }

    public void RefreshExp()
    {
        InGameData playerData = InGameManager.Instance.Controller.Data;

        UIHandler.UpdateGauge(ExpBar, playerData.NextEXP, playerData.Experience);
    }
}