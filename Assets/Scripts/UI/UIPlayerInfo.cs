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
    public Image ReduceHPBar;
    public TextMeshProUGUI HPText;

    public Image ExpBar;

    public void RefreshCurrency(CurrencyTypes type, int value)
    {
        if (type == CurrencyTypes.Gold)
        {
            GoldText.SetText(value.ToString() + "<sprite=0>");
            UIWeaponInfo.RefreshCostColor();
        }
        else if (type == CurrencyTypes.Gem)
        {
            GemText.SetText(value.ToString() + "<sprite=1>");
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
        EventManager<EventTypes>.Register<StatNames>(EventTypes.RefreshPlayerStst, RefreshStat);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister(EventTypes.RefreshPlayerHP, RefreshHp);
        EventManager<EventTypes>.Unregister<StatNames>(EventTypes.RefreshPlayerStst, RefreshStat);
    }

    public void RefreshHp()
    {
        Player player = InGameManager.Instance.Player;

        UIHandler.UpdateGauge(HPbar, player.MaxHP, player.CurrentHp, HPText);
        UIHandler.UpdateGauge(ReduceHPBar, player.MaxHP, player.CurrentHp, HPText);
    }

    public void RefreshStat(StatNames name)
    {
        if (name == StatNames.Invincibility)
        {
            Player player = InGameManager.Instance.Player;

            if (player.StatSystem.GetStat(StatNames.Invincibility) >= 1)
            {
                ReduceHPBar.gameObject.SetActive(true);
            }
            else
            {
                ReduceHPBar.gameObject.SetActive(false);
            }
        }
    }

    public void RefreshExp()
    {
        InGameData playerData = InGameManager.Instance.Controller.Data;

        UIHandler.UpdateGauge(ExpBar, playerData.NextEXP, playerData.Experience);
    }
}