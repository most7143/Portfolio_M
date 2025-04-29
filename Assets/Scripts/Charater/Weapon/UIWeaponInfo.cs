using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponInfo : MonoBehaviour
{
    public XButton UpgradeButton;
    public TextMeshProUGUI WeaponText;
    public TextMeshProUGUI WeaponDamageText;
    public TextMeshProUGUI AttackSpeedText;
    public TextMeshProUGUI CriticalRateText;
    public TextMeshProUGUI CriticalDamageText;

    public TextMeshProUGUI PercentText;
    public TextMeshProUGUI UpgradeGoldText;

    public Image IconImage;

    public RectTransform UIPoint;

    private int currentSucPercent;
    private int baseSucPercent = 95;

    public Player Player
    {
        get { return InGameManager.Instance.Player; }
    }

    private void Start()
    {
        UpgradeButton.OnExecute = Upgrade;
    }

    public void Activate()
    {
        currentSucPercent = baseSucPercent;
        RefreshPercentText();
        RefreshCostColor();
        RefreshUpgradeCostText(1);
        RefreshWeaponInfo(InGameManager.Instance.Player.WeaponController.Info);
    }

    public void Upgrade()
    {
        if (Player == null)
            return;

        int upgradeCost = GetUpgradeCost(Player.WeaponController.Info.Level);

        if (InGameManager.Instance.Controller.Data.Gold < upgradeCost)
        {
            return;
        }

        if (TryUpgrade(Player.WeaponController.Info))
        {
            Player.WeaponController.Info.Level += 1;

            if (TryWeaponUpgradeTier(Player.WeaponController.Info))
            {
                WeaponNames weapon = Player.WeaponController.NextTier(Player.WeaponController.Info.Tier);
                Player.WeaponController.SetWeaponData(weapon);
                UIDetailsManager.Instance.WeaponDetails.Refresh(weapon);
            }

            Player.WeaponController.Info.Damage = GetAddDamage(Player.WeaponController.Info);

            Player.RefreshWeaponInfo();

            RefreshWeaponInfo(Player.WeaponController.Info);

            SetPecent(Player.WeaponController.Info.Level);

            RefreshPercentText();

            RefreshUpgradeCostText(Player.WeaponController.Info.Level);

            RefreshIcon(Player.WeaponController.Info.Icon);

            InGameManager.Instance.ObjectPool.SpawnFloaty(UIPoint.position, FloatyTypes.Success, "성공");
        }
        else
        {
            InGameManager.Instance.ObjectPool.SpawnFloaty(UIPoint.position, FloatyTypes.Fail, "실패");
        }

        InGameManager.Instance.Controller.UseCurrency(CurrencyTypes.Gold, upgradeCost);
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
}