using UnityEngine;

public class Player : Character
{
    public Animator Animator;
    public int NextExp;
    public int CurrentExp;

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