using UnityEngine;

public class Player : Character
{
    public Animator Animator;
    public WeaponController WeaponController;

    public int NextExp;
    public int CurrentExp;

    protected override void Awake()
    {
        WeaponController.SetBaseData();
        RefreshWeaponInfo();
        base.Awake();
    }

    public void RefreshWeaponInfo()
    {
        Damage = WeaponController.Info.Damage;
        AttackSpeed = WeaponController.Info.Speed;
    }

    public override void Attack()
    {
        base.Attack();
        Animator.SetTrigger("Attack");
    }

    public void AnimationAttack()
    {
        Monster target = InGameManager.Instance.Monster;

        if (target != null)
        {
            target.Hit(Damage);
            InGameManager.Instance.ObjectPool.SpawnFloaty(target.transform.position, FloatyTypes.Damage, Damage.ToString());
        }
    }
}