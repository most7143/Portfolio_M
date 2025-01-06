using TMPro;
using UnityEngine;

public class UIPlayerInfo : MonoBehaviour
{
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI WeaponText;
    public TextMeshProUGUI WeaponDamageText;
    public TextMeshProUGUI AttackSpeedText;
    public WeaponUpgrade WeaponUpgrade;

    public void RefreshGold(int gold)
    {
        GoldText.SetText(gold.ToString());

        WeaponUpgrade.RefreshCostColor();
    }

    public void Setup(Player player)
    {
        RefreshWeaponInfo(player.WeaponController.Info);
    }

    public void RefreshWeaponInfo(WeaponInfo weaponInfo)
    {
        WeaponText.SetText("Lv." + weaponInfo.Level + " " + weaponInfo.NameString);
        WeaponDamageText.SetText("공격력 : " + weaponInfo.Damage.ToString());
        AttackSpeedText.SetText("공격속도 : " + weaponInfo.Speed.ToString());
    }
}