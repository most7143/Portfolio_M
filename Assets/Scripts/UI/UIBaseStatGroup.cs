using TMPro;
using UnityEngine;

public class UIBaseStatGroup : MonoBehaviour
{
    public RectTransform Rect;
    public CanvasGroup CanvasGroup;
    public Player Player;

    public TextMeshProUGUI DamageText;
    public TextMeshProUGUI SpeedText;
    public TextMeshProUGUI CriticalRateText;
    public TextMeshProUGUI CriticalDamageText;
    public TextMeshProUGUI ArmorText;
    public TextMeshProUGUI ReduceText;
    public TextMeshProUGUI DodgeRateText;

    private void OnEnable()
    {
        EventManager<EventTypes>.Register<StatNames>(EventTypes.RefreshPlayerStst, Refresh);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister<StatNames>(EventTypes.RefreshPlayerStst, Refresh);
    }

    public void Refresh(StatNames name = StatNames.None)
    {
        DamageText.SetText("공격력 : " + Player.Attack.ToString());
        SpeedText.SetText("공격속도 : " + Player.AttackSpeed.ToString());
        CriticalRateText.SetText("치명타 확률 : " + (Player.StatSystem.GetStat(StatNames.CriticalChance) * 100f).ToString() + "%");
        CriticalDamageText.SetText("치명타 피해 : " + (Player.StatSystem.GetStat(StatNames.CriticalDamage) * 100f).ToString() + "%");
        ArmorText.SetText("방어력 : " + Player.Armor.ToString());
        ReduceText.SetText("피해 감소 : " + ((Player.StatSystem.GetStat(StatNames.DamageReduction) - 1) * 100f).ToString() + "%");
        DodgeRateText.SetText("회피율 : " + (Player.StatSystem.GetStat(StatNames.DodgeRate) * 100f).ToString() + "%");
    }
}