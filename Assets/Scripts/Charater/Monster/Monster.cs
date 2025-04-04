using UnityEngine;

public class Monster : Character
{
    [HideInInspector] public int MaxLevel = 1;

    public MonsterSpanwer Spanwer;

    private MonsterData data;

    public void SetData()
    {
        data = ResourcesManager.Instance.LoadScriptable<MonsterData>(Name.ToString());
        NameString = data.NameString;
        Level = data.Level;
        MaxLevel = data.MaxLevel;

        RefreshHP((int)MaxHP);
        Spanwer.RefreshLevelByData(Level);
    }

    public override void OnAttack()
    {
        base.OnAttack();

        if (InGameManager.Instance.Player.IsAlive)
        {
            DamageInfo info = CalculateDamage();

            InGameManager.Instance.Player.Hit(ref info);
        }
    }

    public override void Hit(ref DamageInfo info)
    {
        base.Hit(ref info);

        UIManager.Instance.MonsterInfo.RefreshHPBar();
    }

    public override void Dead()
    {
        base.Dead();

        if (Level <= MaxLevel)
        {
            Level += 1;
        }

        RefreshHP((int)MaxHP);

        if (Level > MaxLevel)
        {
            InGameManager.Instance.MonsterSpanwer.Respawn(Level);
        }
        else
        {
            Spanwer.RefreshLevelByData(Level);
            UIManager.Instance.MonsterInfo.Refresh(this);
        }

        InGameManager.Instance.Controller.AddGold(Spanwer.Gold);

        InGameManager.Instance.Controller.AddExp(Spanwer.EXP);

        InGameManager.Instance.RefreshStage(Level);
    }
}