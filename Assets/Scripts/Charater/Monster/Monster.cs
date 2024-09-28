using UnityEngine;

public class Monster : Character
{
    public int Exp = 1;

    public MonsterData data;

    private void Start()
    {
        GetData();
    }

    public void GetData()
    {
        data = Resources.Load<MonsterData>("ScriptableObject/" + Name.ToString());
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