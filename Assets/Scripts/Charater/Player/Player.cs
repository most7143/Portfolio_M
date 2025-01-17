using UnityEngine;

public class Player : Character
{
    public Animator Animator;
    public WeaponController WeaponController;

    public int NextExp;
    public int CurrentExp;

    private void Start()
    {
        WeaponController.InitWeaponDatas();
        RefreshWeaponInfo();
    }

    public void RefreshWeaponInfo()
    {
        Damage = WeaponController.Info.Damage;
        AttackSpeed = WeaponController.Info.Speed;
        Animator.SetFloat("AttackSpeed", AttackSpeed * 2);
    }

    public override void Attack()
    {
        base.Attack();
        Animator.SetTrigger("Attack");
    }

    public override void Hit(float damage)
    {
        base.Hit(damage);

        UIManager.Instance.PlayerInfo.RefreshHp(this);
    }

    public override void Dead()
    {
        base.Dead();
        IsAlive = false;
        InGameManager.Instance.StageFail();
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