using TMPro;
using UnityEngine;

public class UIBaseStatGroup : MonoBehaviour
{
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
        EventManager<EventTypes>.Register(EventTypes.RefreshPlayerStst, Refresh);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister(EventTypes.RefreshPlayerStst, Refresh);
    }

    public void Refresh()
    {
        DamageText.SetText("���ݷ� : " + Player.Attack.ToString());
        SpeedText.SetText("���ݼӵ� : " + Player.AttackSpeed.ToString());
        CriticalRateText.SetText("ġ��Ÿ Ȯ�� : " + (Player.StatSystem.GetStat(StatNames.CriticalChance) * 100f).ToString() + "%");
        CriticalDamageText.SetText("ġ��Ÿ ���� : " + (Player.StatSystem.GetStat(StatNames.CriticalDamage) * 100f).ToString() + "%");
        ArmorText.SetText("���� : " + Player.Armor.ToString());
        ReduceText.SetText("���� ���� : " + ((Player.StatSystem.GetStat(StatNames.DamageReduction) - 1) * 100f).ToString() + "%");
        DodgeRateText.SetText("ȸ���� : " + (Player.StatSystem.GetStat(StatNames.DodgeRate) * 100f).ToString() + "%");
    }
}