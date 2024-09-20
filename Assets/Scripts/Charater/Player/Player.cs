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
        InGameManager.Instance.Monster.Hit(Damage);
    }
}