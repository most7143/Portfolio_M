using UnityEngine;

public class Player : Character
{
    public Animator Animator;
    public WeaponController WeaponController;

    public Monster TargetMonster;

    private void Start()
    {
        WeaponController.InitWeaponDatas();
        RefreshWeaponInfo();

        baseMaxHp = MaxHp;
    }

    public void RefreshWeaponInfo()
    {
        Damage = WeaponController.Info.Damage;
        AttackSpeed = WeaponController.Info.Speed;
        CriticalRage = WeaponController.Info.CriticalRate;
        CriticalDamage = WeaponController.Info.CriticalDamage;

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
        }
    }
}