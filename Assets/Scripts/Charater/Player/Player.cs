using UnityEngine;

public class Player : Character
{
    public Animator Animator;
    public WeaponController WeaponController;
    public Monster TargetMonster;
    public Transform AttackPoint;

    private void Start()
    {
        WeaponController.InitWeaponDatas();
        WeaponController.SetWeaponData(WeaponNames.WoodenSword);
        RefreshWeaponInfo();

        baseMaxHp = MaxHp;
    }

    public void RefreshWeaponInfo()
    {
        Damage = WeaponController.Info.Damage;
        AttackSpeed = WeaponController.Info.Speed;
        StatSystem.SetStat(StatNames.CriticalChance, WeaponController.Info.CriticalRate);
        StatSystem.SetStat(StatNames.CriticalDamage, WeaponController.Info.CriticalDamage);

        Animator.SetFloat("AttackSpeed", AttackSpeed * 2);
    }

    public override void Attack()
    {
        base.Attack();
        Animator.SetTrigger("Attack");
    }

    public override void Hit(DamageInfo info)
    {
        base.Hit(info);

        UIManager.Instance.PlayerInfo.RefreshHp(this);
    }

    public override void Dead()
    {
        base.Dead();
        IsAlive = false;
        InGameManager.Instance.StageFail();
    }

    public void LevelUp()
    {
        Level += 1;

        MaxHp = baseMaxHp * Level;
        CurrentHp = MaxHp;

        UIManager.Instance.PlayerInfo.RefreshHp(this);
        UIManager.Instance.PlayerInfo.RefreshLevel(Level);
    }

    public void SkillAttack(DamageInfo info)
    {
        TargetMonster.Hit(info);

        Vector3 position = new Vector3(TargetMonster.transform.position.x + Random.Range(0, 0.5f), TargetMonster.transform.position.y + Random.Range(0, 0.5f));

        InGameManager.Instance.ObjectPool.SpawnFloaty(position, FloatyTypes.SkillDamage, info.Value.ToString());
    }

    public void AnimationAttack()
    {
        if (TargetMonster == null)
        {
            TargetMonster = InGameManager.Instance.Monster;
        }

        if (TargetMonster != null)
        {
            DamageInfo info = CalculateDamage();
            TargetMonster.Hit(info);

            if (info.IsCritical)
            {
                InGameManager.Instance.ObjectPool.SpawnFloaty(TargetMonster.transform.position, FloatyTypes.CritialDamage, info.Value.ToString());
            }
            else
            {
                InGameManager.Instance.ObjectPool.SpawnFloaty(TargetMonster.transform.position, FloatyTypes.Damage, info.Value.ToString());
            }

            EventManager<EventTypes>.Send(EventTypes.AttackExecuted, WeaponController.Name);
        }
    }
}