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
        DamageText.SetText(EXText.GetStatLanguage(StatNames.Attack) + " : " + EXText.GetStatPercent(StatNames.Attack, Player.Attack));
        SpeedText.SetText(EXText.GetStatLanguage(StatNames.AttackSpeed) + " : " + EXText.GetStatPercent(StatNames.AttackSpeed, Player.AttackSpeed));
        CriticalRateText.SetText(EXText.GetStatLanguage(StatNames.CriticalChance) + " : " + EXText.GetStatPercent(StatNames.CriticalChance, Player.StatSystem.GetStat(StatNames.CriticalChance)));
        CriticalDamageText.SetText(EXText.GetStatLanguage(StatNames.CriticalDamage) + " : " + EXText.GetStatPercent(StatNames.CriticalDamage, Player.StatSystem.GetStat(StatNames.CriticalDamage)));
        ArmorText.SetText(EXText.GetStatLanguage(StatNames.Armor) + " : " + EXText.GetStatPercent(StatNames.Armor, Player.Armor));
        ReduceText.SetText(EXText.GetStatLanguage(StatNames.DamageReduction) + " : " + EXText.GetStatPercent(StatNames.DamageReduction, Player.StatSystem.GetStat(StatNames.DamageReduction) - 1));
        DodgeRateText.SetText(EXText.GetStatLanguage(StatNames.DodgeRate) + " : " + EXText.GetStatPercent(StatNames.DodgeRate, Player.StatSystem.GetStat(StatNames.DodgeRate)));
    }
}