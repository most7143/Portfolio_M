using TMPro;
using UnityEngine;

public class UIPlayerInfo : MonoBehaviour
{
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI WeaponText;
    public TextMeshProUGUI WeaponDamageText;
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
        WeaponDamageText.SetText("°ø°Ý·Â : " + weaponInfo.Damage.ToString());
    }
}