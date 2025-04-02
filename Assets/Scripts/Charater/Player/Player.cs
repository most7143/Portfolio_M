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

        CurrentHp = MaxHP;
    }

    public void RefreshWeaponInfo()
    {
        StatSystem.RemoveStat(StatTID.Weapon, StatNames.CriticalChance);
        StatSystem.RemoveStat(StatTID.Weapon, StatNames.CriticalDamage);
        StatSystem.RemoveStat(StatTID.Weapon, StatNames.Attack);
        StatSystem.RemoveStat(StatTID.Weapon, StatNames.AttackSpeed);

        StatSystem.AddStat(StatTID.Weapon, StatNames.CriticalChance, WeaponController.Info.CriticalRate);
        StatSystem.AddStat(StatTID.Weapon, StatNames.CriticalDamage, WeaponController.Info.CriticalDamage);
        StatSystem.AddStat(StatTID.Weapon, StatNames.Attack, WeaponController.Info.Damage);
        StatSystem.AddStat(StatTID.Weapon, StatNames.AttackSpeed, WeaponController.Info.Speed);

        Animator.SetFloat("AttackSpeed", AttackSpeed * 2);
    }

    public override void OnAttack()
    {
        base.OnAttack();
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
        CurrentHp = MaxHP;

        UIManager.Instance.PlayerInfo.RefreshHp(this);

        EventManager<EventTypes>.Send(EventTypes.LevelUp, Level);
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