using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgrade : MonoBehaviour
{
    public Button UprageButton;

    public TextMeshProUGUI PercentText;
    public TextMeshProUGUI CurrentGoldText;
    public TextMeshProUGUI UpgradeGoldText;

    public Image IconImage;

    private int currentSucPercent;
    private int baseSucPercent = 95;

    private void Start()
    {
        InGameManager.Instance.Controller.Data.Gold += 100000;
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
            player.WeaponController.Info.Level += 1;

            if (TryWeaponUpgradeTier(player.WeaponController.Info))
            {
                WeaponNames weapon = player.WeaponController.NextTier(player.WeaponController.Info.Tier);
                player.WeaponController.SetWeaponData(weapon);
            }

            player.WeaponController.Info.Damage = GetAddDamage(player.WeaponController.Info);

            player.RefreshWeaponInfo();
            InGameManager.Instance.PlayerInfo.RefreshWeaponInfo(player.WeaponController.Info);

            SetPecent(player.WeaponController.Info.Level);

            RefreshPercentText();

            RefreshUpgradeCostText(player.WeaponController.Info.Level);

            RefreshIcon(player.WeaponController.Info.Icon);
        }

        InGameManager.Instance.Controller.UseGold(upgradeCost);
    }

    private bool TryWeaponUpgradeTier(WeaponInfo weaponInfo)
    {
        if (weaponInfo.Name == WeaponNames.DoomsDay)
            return false;

        if (weaponInfo.Level == weaponInfo.Tier * 10)
        {
            return true;
        }

        return false;
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
        return weaponInfo.Level * weaponInfo.LevelByBonus;
    }

    private void SetPecent(int level)
    {
        int percent = baseSucPercent - (4 * (level / 5));

        if (percent < 4)
        {
            percent = 4;
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

    private void RefreshIcon(Sprite icon)
    {
        IconImage.sprite = icon;
    }
}