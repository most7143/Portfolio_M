using UnityEngine;

public class Monster : Character
{
    [HideInInspector] public int MaxLevel = 1;

    public MonsterSpanwer Spanwer;

    private MonsterData data;

    public void SetData(float damage, float maxHp)
    {
        data = ResourcesManager.Instance.LoadScriptable<MonsterData>(Name.ToString());
        NameString = data.NameString;
        Level = data.Level;
        MaxLevel = data.MaxLevel;
        Damage = damage;
        MaxHp = maxHp;
        CurrentHp = MaxHp;
        AttackSpeed = data.AttackSpeed;

        Spanwer.RefreshLevelByData(Level);
    }

    public override void Attack()
    {
        base.Attack();

        if (InGameManager.Instance.Player.IsAlive)
        {
            DamageInfo info = CalculateDamage();

            InGameManager.Instance.Player.Hit(info);
        }
    }

    public override void Hit(DamageInfo info)
    {
        base.Hit(info);

        UIManager.Instance.MonsterInfo.RefreshHPBar();
    }

    public override void Dead()
    {
        base.Dead();

        RefreshData();

        if (Level <= MaxLevel)
        {
            Level += 1;
        }

        if (Level > MaxLevel && Name + 1 != CharacterNames.End)
        {
            InGameManager.Instance.MonsterSpanwer.Respawn(Name + 1);
        }
        else
        {
            Spanwer.RefreshLevelByData(Level);
            UIManager.Instance.MonsterInfo.Refresh(this);
        }

        InGameManager.Instance.Controller.AddGold(Spanwer.Gold);

        InGameManager.Instance.Controller.AddExp(Spanwer.EXP);

        InGameManager.Instance.RefreshStage(Level);
    }

    private void RefreshData()
    {
        Damage *= Spanwer.DamageByLevel;
        MaxHp *= Spanwer.MaxHPMultiplierByLevel;
        Damage = Mathf.CeilToInt(Damage);

        MaxHp = Mathf.RoundToInt(MaxHp);

        Spanwer.MaxHP = MaxHp;
        Spanwer.Damage = Damage;

        CurrentHp = MaxHp;
    }
}