public class Monster : Character
{
    public int Exp = 1;

    public override void Attack()
    {
        base.Attack();

        InGameManager.Instance.Player.Hit(Damage);
    }
}