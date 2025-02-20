using UnityEngine;

public class Player : Character
{
    public Animator Animator;
    public WeaponController WeaponController;
    public FXManager FXManager;

    private Monster targetMonster;

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
        if (targetMonster == null)
        {
            targetMonster = InGameManager.Instance.Monster;
        }

        if (targetMonster != null)
        {
            DamageInfo info = CalculateDamage();
            targetMonster.Hit(info);

            if (info.IsCritical)
            {
                InGameManager.Instance.ObjectPool.SpawnFloaty(targetMonster.transform.position, FloatyTypes.CritialDamage, info.Value.ToString());
            }
            else
            {
                InGameManager.Instance.ObjectPool.SpawnFloaty(targetMonster.transform.position, FloatyTypes.Damage, info.Value.ToString());
            }

            AnimationFX();
        }
    }

    public void AnimationFX()
    {
        if (targetMonster == null)
        {
            targetMonster = InGameManager.Instance.Monster;
        }

        if (WeaponController.Info.FXName != FXNames.None
            && targetMonster != null)
        {
            FXManager.Spawn(WeaponController.Info.FXName, targetMonster.transform.position);
        }
    }
}