public class Monster : Character
{
    public int Exp = 1;

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
}