using UnityEngine;

public class Monster : Character
{
    [HideInInspector] public int MaxLevel = 1;

    public MonsterSpanwer Spanwer;

    private MonsterData data;

    public void SetData(MonsterData monsterData)
    {
        data = monsterData;
        NameString = data.NameText;
        Level = data.Level;
        MaxLevel = data.MaxLevel;

        StatSystem.Clear();

        StatSystem.AddStat(StatTID.Base, StatNames.Attack, data.Attack);
        StatSystem.AddStat(StatTID.Base, StatNames.AttackRate, 1);
        StatSystem.AddStat(StatTID.Base, StatNames.AttackByLevel, data.AttackByLevel);

        StatSystem.AddStat(StatTID.Base, StatNames.AttackSpeed, data.AttackSpeed);

        StatSystem.AddStat(StatTID.Base, StatNames.Health, data.Health);
        StatSystem.AddStat(StatTID.Base, StatNames.HealthRate, 1);
        StatSystem.AddStat(StatTID.Base, StatNames.HealthByLevel, data.HealthByLevel);

        StatSystem.AddStat(StatTID.Base, StatNames.Armor, data.Armor);
        StatSystem.AddStat(StatTID.Base, StatNames.ArmorRate, 1);
        StatSystem.AddStat(StatTID.Base, StatNames.ArmorByLevel, data.ArmorByLevel);

        StatSystem.AddStat(StatTID.Base, StatNames.DamageReduction, 1);
        StatSystem.AddStat(StatTID.Base, StatNames.DamageRate, 1);

        RefreshHP((int)MaxHP);

        Spanwer.RefreshLevelByData(Level);
    }

    public override void OnAttack()
    {
        base.OnAttack();

        if (InGameManager.Instance.Player.IsAlive)
        {
            DamageInfo info = CalculateDamage();

            InGameManager.Instance.Player.Hit(ref info);
        }
    }

    public override bool Hit(ref DamageInfo info)
    {
        if (base.Hit(ref info))
        {
            UIManager.Instance.MonsterInfo.RefreshHPBar();
        }

        return true;
    }

    public override void Dead()
    {
        base.Dead();

        if (Level <= MaxLevel)
        {
            Level += 1;
        }

        RefreshHP((int)MaxHP);

        if (Level > MaxLevel)
        {
            InGameManager.Instance.MonsterSpanwer.ChangeMonsterSkin(Level);
        }

        Spanwer.RefreshLevelByData(Level);

        UIManager.Instance.MonsterInfo.Refresh(this);

        DropCurrency();

        InGameManager.Instance.Controller.AddExp(Spanwer.EXP);

        InGameManager.Instance.RefreshStage(Level);

        EventManager<EventTypes>.Send(EventTypes.MonsterDead);
    }

    private void DropCurrency()
    {
        float rand = Random.Range(0, 1f);

        if (0.5f >= rand)
        {
            InGameManager.Instance.Controller.AddCurrencyAnim(CurrencyTypes.Gem, transform.position, Spanwer.Gem);
        }

        InGameManager.Instance.Controller.AddCurrencyAnim(CurrencyTypes.Gold, transform.position, Spanwer.Gold);
    }
}