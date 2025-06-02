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

    private float currentSucPercent = 0.95f;

    public float[] _percentChances = { 0.95f, 0.90f, 0.85f, 0.8f, 0.7f, 0.6f, 0.5f, 0.4f, 0.3f, 0.15f };

    public Player Player
    {
        get { return InGameManager.Instance.Player; }
    }

    public void OnEnable()
    {
        EventManager<EventTypes>.Register<ClassNames>(EventTypes.ChangeClass, ChangeClass);
    }

    public void OnDisable()
    {
        EventManager<EventTypes>.Unregister<ClassNames>(EventTypes.ChangeClass, ChangeClass);
    }

    private void Start()
    {
        UpgradeButton.OnExecute = Upgrade;
    }

    public void Activate()
    {
        RefreshPercentText();
        RefreshCostColor();
        RefreshUpgradeCostText(Player.WeaponController.Info.Level);
        RefreshWeaponInfoText(InGameManager.Instance.Player.WeaponController.Info);
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
                SpawnDetails(weapon);
                SoundManager.Instance.Play(SoundNames.WeaponUpgrade);
            }

            Player.WeaponController.RefreahWeaponStat();

            Player.RefreshWeaponInfo();

            RefreshWeaponInfoText(Player.WeaponController.Info);

            SetPecent(Player.WeaponController.Info.Level);

            RefreshPercentText();

            RefreshUpgradeCostText(Player.WeaponController.Info.Level);

            RefreshIcon(Player.WeaponController.Info.Icon);

            InGameManager.Instance.ObjectPool.SpawnFloaty(UIPoint.position, FloatyTypes.Success, "����");
            SoundManager.Instance.Play(SoundNames.Success);
        }
        else
        {
            if (InGameManager.Instance.Player.StatSystem.GetStat(StatNames.FailToWeaponChance) > 0)
            {
                currentSucPercent += InGameManager.Instance.Player.StatSystem.GetStat(StatNames.FailToWeaponChance);
                RefreshPercentText();
            }

            InGameManager.Instance.ObjectPool.SpawnFloaty(UIPoint.position, FloatyTypes.Fail, "����");
        }

        InGameManager.Instance.Controller.UseCurrency(CurrencyTypes.Gold, upgradeCost);
    }

    public void ChangeClass(ClassNames name)
    {
        if (name == ClassNames.MasterCraftsman)
        {
            Player.WeaponController.RefreahWeaponStat();

            Player.RefreshWeaponInfo();
        }
    }

    private void SpawnDetails(WeaponNames name)
    {
        UIDetailsManager.Instance.Spawn(UIDetailsNames.WeaponUpgrade);
        UIWeaponDetails details = UIDetailsManager.Instance.Details.GetComponent<UIWeaponDetails>();

        if (details != null)
        {
            details.Refresh(name);
        }
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
        return (int)(weaponLevel * 10 * (1 - Player.StatSystem.GetStat(StatNames.DecreaseWeaponUpgradeCost)));
    }

    private bool TryUpgrade(WeaponInfo weaponInfoupgradeCost)
    {
        float range = Random.Range(0, 1f);

        return range < currentSucPercent;
    }

    private void SetPecent(int level)
    {
        int onesDigit = level % 10;

        if (level <= 0)
            onesDigit = 0;

        currentSucPercent = _percentChances[onesDigit];
    }

    private void RefreshPercentText()
    {
        PercentText.SetText("���� Ȯ�� : " + currentSucPercent * 100f + "%");
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

    public void RefreshWeaponInfoText(WeaponInfo weaponInfo)
    {
        WeaponText.SetText("Lv." + weaponInfo.Level + " " + weaponInfo.NameText);
        WeaponDamageText.SetText("���ݷ� : " + RefreshColorByDamage(weaponInfo.Damage));
        AttackSpeedText.SetText("���ݼӵ� : " + weaponInfo.Speed.ToString());
        CriticalRateText.SetText("ġ��Ÿ Ȯ�� : " + RefreshColorByCriticalRate(weaponInfo.CriticalRate * 100f));
        CriticalDamageText.SetText("ġ��Ÿ ������ : " + RefreshColorByCriticalDamage(weaponInfo.CriticalDamage * 100f));
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