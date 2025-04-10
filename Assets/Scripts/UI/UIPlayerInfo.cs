using Assets.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInfo : MonoBehaviour
{
    public TextMeshProUGUI GoldText;

    public UIWeaponInfo UIWeaponInfo;

    public UIPassiveSkillInfo UIPassiveSkillInfo;

    public Image HPbar;
    public TextMeshProUGUI HPText;

    public Image ExpBar;

    public void RefreshGold(int gold)
    {
        GoldText.SetText(gold.ToString());

        UIWeaponInfo.RefreshCostColor();
    }

    public void Setup(Player player)
    {
        RefreshHp();
        RefreshExp();
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