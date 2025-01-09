using UnityEngine;

public class Monster : Character
{
    [HideInInspector] public int Exp = 1;

    [HideInInspector] public int Gold = 1;

    [HideInInspector] public int MaxLevel = 1;

    public MonsterData data;

    protected override void Awake()
    {
        SetData();
        base.Awake();
    }

    public void SetData()
    {
        data = Resources.Load<MonsterData>("ScriptableObject/Monster/" + Name.ToString());
        NameString = data.NameString;
        Level = data.Level;
        MaxLevel = data.MaxLevel;
        MaxHp = data.MaxHP;
        CurrentHp = MaxHp;
        Damage = data.Damage;
        AttackSpeed = data.AttackSpeed;
        Exp = data.EXP;
        Gold = data.Gold;
    }

    public override void Attack()
    {
        base.Attack();

        InGameManager.Instance.Player.Hit(Damage);
    }

    public override void Hit(float damage)
    {
        base.Hit(damage);

        InGameManager.Instance.MonsterInfo.RefreshHPBar();
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

            InGameManager.Instance.MonsterInfo.Refresh(this);
        }

        InGameManager.Instance.Controller.AddGold(Gold);

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