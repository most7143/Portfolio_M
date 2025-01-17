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
        WeaponDamageText.SetText("공격력 : " + weaponInfo.Damage.ToString());
        AttackSpeedText.SetText("공격속도 : " + weaponInfo.Speed.ToString());
    }

    public void RefreshLevel(int level)
    {
        LevelText.SetText("Lv." + level);
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