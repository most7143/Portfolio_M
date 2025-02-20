using UnityEngine;

public class Monster : Character
{
    [HideInInspector] public int MaxLevel = 1;

    public MonsterSpanwer Spanwer;

    private MonsterData data;

    public void SetData()
    {
        data = ResourcesManager.Instance.LoadScriptable<MonsterData>(Name.ToString());
        NameString = data.NameString;
        Level = data.Level;
        MaxLevel = data.MaxLevel;
        MaxHp = data.MaxHP;
        CurrentHp = MaxHp;
        Damage = data.Damage;
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
            MaxHp = GetHP();
            CurrentHp = MaxHp;
            Damage = (Level - data.Level) * data.DamageByLevel;
            Spanwer.RefreshLevelByData(Level);
            UIManager.Instance.MonsterInfo.Refresh(this);
        }

        InGameManager.Instance.Controller.AddGold(Spanwer.Gold);

        InGameManager.Instance.Controller.AddExp(Spanwer.EXP);

        InGameManager.Instance.RefreshStage(Level);
    }

    private float GetHP()
    {
        int level = Level - data.Level;

        float maxHp = data.MaxHP;

        for (int i = 0; i < level; i++)
        {
            maxHp *= data.MaxHPMultiplierByLevel;
        }

        return Mathf.Round(maxHp);
    }
}