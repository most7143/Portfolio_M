using Assets.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInfo : MonoBehaviour
{
    public TextMeshProUGUI GemText;

    public TextMeshProUGUI GoldText;

    public UIWeaponInfo UIWeaponInfo;

    public UIPassiveSkillInfo UIPassiveSkillInfo;

    public Image HPbar;
    public TextMeshProUGUI HPText;

    public Image ExpBar;

    public void RefreshCurrency(CurrencyTypes type, int value)
    {
        if (type == CurrencyTypes.Gold)
        {
            GoldText.SetText(value.ToString());
            UIWeaponInfo.RefreshCostColor();
        }
        else if (type == CurrencyTypes.Gem)
        {
            GemText.SetText(value.ToString());
        }
    }

    public void Setup(Player player)
    {
        RefreshHp();
        RefreshExp();

        RefreshCurrency(CurrencyTypes.Gold, InGameManager.Instance.Controller.Data.Gold);
        RefreshCurrency(CurrencyTypes.Gem, InGameManager.Instance.Controller.Data.Gem);
    }

    private void OnEnable()
    {
        EventManager<EventTypes>.Register(EventTypes.RefreshPlayerHP, RefreshHp);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister(EventTypes.RefreshPlayerHP, RefreshHp);
    }

    public void RefreshHp()
    {
        Player player = InGameManager.Instance.Player;

        UIHandler.UpdateGauge(HPbar, player.MaxHP, player.CurrentHp, HPText);
    }

    public void RefreshExp()
    {
        InGameData playerData = InGameManager.Instance.Controller.Data;

        UIHandler.UpdateGauge(ExpBar, playerData.NextEXP, playerData.Experience);
    }
}