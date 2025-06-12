using System.Collections;
using UnityEngine;

public class Monster : Character
{
    [HideInInspector] public int MaxLevel = 1;

    public MonsterSpanwer Spanwer;

    public EliteTypes EliteType;

    private MonsterData data;

    public void SetData(MonsterData monsterData)
    {
        data = monsterData;
        NameString = data.NameText;
        Level = data.Level;
        MaxLevel = data.MaxLevel;

        StatSystem.Clear();

        StatSystem.AddStat(StatTID.Base, StatNames.Attack, data.AttackByLevel);
        StatSystem.AddStat(StatTID.Base, StatNames.AttackRate, 1);
        StatSystem.AddStat(StatTID.Base, StatNames.AttackByLevel, data.AttackByLevel);

        StatSystem.AddStat(StatTID.Base, StatNames.AttackSpeed, data.AttackSpeed);

        StatSystem.AddStat(StatTID.Base, StatNames.CriticalChance, 0.05f);
        StatSystem.AddStat(StatTID.Base, StatNames.CriticalDamage, 2f);

        StatSystem.AddStat(StatTID.Base, StatNames.Health, data.HealthByLevel);
        StatSystem.AddStat(StatTID.Base, StatNames.HealthRate, 1);
        StatSystem.AddStat(StatTID.Base, StatNames.HealthByLevel, data.HealthByLevel);

        StatSystem.AddStat(StatTID.Base, StatNames.Armor, data.ArmorByLevel);
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

        float exp = Spanwer.EXP;

        if (EliteType != EliteTypes.None)
        {
            RemoveEliteStat();
            EliteType = EliteTypes.None;
            exp *= 2;
            StartCoroutine(DropProcess());
        }
        else
        {
            DropCurrency();
        }

        if (TryElite())
        {
            AddEliteStat();
        }

        if (Level > MaxLevel)
        {
            InGameManager.Instance.MonsterSpanwer.ChangeMonsterSkin(Level);
        }

        Spanwer.RefreshLevelByData(Level);

        RefreshHP((int)MaxHP);

        InGameManager.Instance.Controller.AddExp(exp);

        InGameManager.Instance.RefreshStage(Level);

        UIManager.Instance.MonsterInfo.Refresh(this);

        InGameManager.Instance.Player.HealRate(0.2f, false);

        OutGameManager.Instance.TotalMonsterKillCount += 1;

        SoundManager.Instance.Play(SoundNames.Dead);

        EventManager<EventTypes>.Send(EventTypes.MonsterDead);
    }

    private bool TryElite()
    {
        if (EliteType == EliteTypes.None)
        {
            if (Level >= Spanwer.MinEliteLevel)
            {
                float rand = Random.Range(0, 1f);

                if (rand < Spanwer.EliteChance)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void AddEliteStat()
    {
        EliteTypes[] types = (EliteTypes[])System.Enum.GetValues(typeof(EliteTypes));

        int rand = Random.Range(1, types.Length);

        EliteType = types[rand];

        switch (EliteType)
        {
            case EliteTypes.Powerful:
                {
                    StatSystem.AddStat(StatTID.Elite, StatNames.DamageRate, 0.5f);
                }
                break;

            case EliteTypes.Armored:
                {
                    StatSystem.AddStat(StatTID.Elite, StatNames.DamageReduction, 0.5f);
                }

                break;

            case EliteTypes.Resilient:
                {
                    StatSystem.AddStat(StatTID.Elite, StatNames.HealthRate, 2f);
                }
                break;

            case EliteTypes.Agile:
                {
                    StatSystem.AddStat(StatTID.Elite, StatNames.DodgeRate, 0.5f);
                }
                break;

            case EliteTypes.Deadly:
                {
                    StatSystem.AddStat(StatTID.Elite, StatNames.CriticalChance, 0.6f);
                    StatSystem.AddStat(StatTID.Elite, StatNames.CriticalDamage, 1f);
                }
                break;
        }
    }

    private void RemoveEliteStat()
    {
        switch (EliteType)
        {
            case EliteTypes.Powerful:
                {
                    StatSystem.RemoveStat(StatTID.Elite, StatNames.DamageRate);
                }
                break;

            case EliteTypes.Armored:
                {
                    StatSystem.RemoveStat(StatTID.Elite, StatNames.DamageReduction);
                }

                break;

            case EliteTypes.Resilient:
                {
                    StatSystem.RemoveStat(StatTID.Elite, StatNames.HealthRate);
                }
                break;

            case EliteTypes.Agile:
                {
                    StatSystem.RemoveStat(StatTID.Elite, StatNames.DodgeRate);
                }
                break;

            case EliteTypes.Deadly:
                {
                    StatSystem.RemoveStat(StatTID.Elite, StatNames.CriticalChance);
                    StatSystem.RemoveStat(StatTID.Elite, StatNames.CriticalDamage);
                }
                break;
        }
    }

    private void DropCurrency()
    {
        float rand = Random.Range(0, 1f);

        if (0.3f >= rand)
        {
            InGameManager.Instance.Controller.AddCurrencyAnim(CurrencyTypes.Gem, transform.position, Spanwer.Gem);
        }

        InGameManager.Instance.Controller.AddCurrencyAnim(CurrencyTypes.Gold, transform.position, Spanwer.Gold);
    }

    private IEnumerator DropProcess()
    {
        for (int i = 0; i < 6; i++)
        {
            DropCurrency();
            yield return new WaitForSeconds(0.1f);
        }
    }
}