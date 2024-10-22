using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgrade : MonoBehaviour
{
    public Button UprageButton;

    public TextMeshProUGUI PercentText;
    public TextMeshProUGUI CurrentGoldText;
    public TextMeshProUGUI UpgradeGoldText;

    private int currentSucPercent;
    private int baseSucPercent = 95;

    private void Start()
    {
        UprageButton.onClick.AddListener(Upgrade);
        currentSucPercent = baseSucPercent;
        RefreshPercentText();
        RefreshCostColor();
        RefreshUpgradeCostText(1);
    }

    public void Upgrade()
    {
        Player player = InGameManager.Instance.Player;

        if (player == null)
            return;

        int upgradeCost = GetUpgradeCost(player.WeaponController.Info.Level);

        if (TryUpgrade(player.WeaponController.Info, upgradeCost))
        {
            player.WeaponController.Info.Damage += GetAddDamage(player.WeaponController.Info);
            player.WeaponController.Info.Level += 1;

            player.RefreshWeaponInfo();
            InGameManager.Instance.PlayerInfo.RefreshWeaponInfo(player.WeaponController.Info);

            SetPecent(player.WeaponController.Info.Level);

            RefreshPercentText();

            RefreshUpgradeCostText(player.WeaponController.Info.Level);
        }

        InGameManager.Instance.Controller.UseGold(upgradeCost);
    }

    private int GetUpgradeCost(int weaponLevel)
    {
        return weaponLevel * 2;
    }

    private bool TryUpgrade(WeaponInfo weaponInfo, int upgradeCost)
    {
        if (InGameManager.Instance.Controller.Data.Gold >= upgradeCost)
        {
            int range = Random.Range(0, 100);

            return range < currentSucPercent;
        }

        return false;
    }

    public int GetAddDamage(WeaponInfo weaponInfo)
    {
        return 1 + (weaponInfo.Level / weaponInfo.LevelByBonus);
    }

    private void SetPecent(int level)
    {
        int percent = baseSucPercent - (5 * (level / 3));

        if (percent < 5)
        {
            percent = 5;
        }

        currentSucPercent = percent;
    }

    private void RefreshPercentText()
    {
        PercentText.SetText("¼º°ø È®·ü : " + currentSucPercent.ToString() + "% \n"
            + "½ÇÆÐ È®·ü : " + (100 - currentSucPercent).ToString() + "%");
    }

    public void RefreshCostColor()
    {
        int currentGold = InGameManager.Instance.Controller.Data.Gold;

        Player player = InGameManager.Instance.Player;

        if (currentGold >= GetUpgradeCost(player.WeaponController.Info.Level))
        {
            UpgradeGoldText.color = Color.white;
        }
        else
        {
            UpgradeGoldText.color = Color.red;
        }
    }

    private void RefreshUpgradeCostText(int weaponLevel)
    {
        UpgradeGoldText.SetText(GetUpgradeCost(weaponLevel).ToString());
    }
}