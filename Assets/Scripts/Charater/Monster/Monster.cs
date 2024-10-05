using UnityEngine;

public class Monster : Character
{
    public int Exp = 1;

    public int Gold = 1;

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
        MaxHp = data.MaxHP;
        CurrentHp = MaxHp;
        Damage = data.Damage;
        AttackSpeed = data.AttackSpeed;
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
        Level += 1;

        MaxHp = GetHP();
        CurrentHp = MaxHp;
        Damage = (Level - data.Level) * data.DamageByLevel;

        InGameManager.Instance.MonsterInfo.Refresh();
        InGameManager.Instance.Controller.AddGold(Gold);
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