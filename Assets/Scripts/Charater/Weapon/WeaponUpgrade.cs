using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgrade : MonoBehaviour
{
    public CanvasGroup Group;
    public UpgradeButton UprageButton;

    public TextMeshProUGUI PercentText;
    public TextMeshProUGUI UpgradeGoldText;

    public Image IconImage;

    public RectTransform UIPoint;

    private int currentSucPercent;
    private int baseSucPercent = 95;

    private void Start()
    {
        InGameManager.Instance.Controller.Data.Gold += 100000;
    }

    public void Activate()
    {
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

        if (InGameManager.Instance.Controller.Data.Gold < upgradeCost)
        {
            return;
        }

        if (TryUpgrade(player.WeaponController.Info))
        {
            player.WeaponController.Info.Level += 1;

            if (TryWeaponUpgradeTier(player.WeaponController.Info))
            {
                WeaponNames weapon = player.WeaponController.NextTier(player.WeaponController.Info.Tier);
                player.WeaponController.SetWeaponData(weapon);
                UIDetailsManager.Instance.WeaponDetails.Refresh(weapon);
            }

            player.WeaponController.Info.Damage = GetAddDamage(player.WeaponController.Info);

            player.RefreshWeaponInfo();

            UIManager.Instance.PlayerInfo.RefreshWeaponInfo(player.WeaponController.Info);

            SetPecent(player.WeaponController.Info.Level);

            RefreshPercentText();

            RefreshUpgradeCostText(player.WeaponController.Info.Level);

            RefreshIcon(player.WeaponController.Info.Icon);

            InGameManager.Instance.ObjectPool.SpawnFloaty(UIPoint.position, FloatyTypes.Success, "성공");
        }
        else
        {
            InGameManager.Instance.ObjectPool.SpawnFloaty(UIPoint.position, FloatyTypes.Fail, "실패");
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

    private bool TryUpgrade(WeaponInfo weaponInfoupgradeCost)
    {
        int range = Random.Range(0, 100);

        return range < currentSucPercent;
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
        PercentText.SetText("성공 확률 : " + currentSucPercent.ToString() + "% \n"
            + "실패 확률 : " + (100 - currentSucPercent).ToString() + "%");
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