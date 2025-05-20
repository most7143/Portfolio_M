using UnityEngine;

public class Player : Character
{
    public Animator Animator;
    public WeaponController WeaponController;
    public Transform AttackPoint;

    public AccessorySystem AccessorySystem;

    public ClassTraitSystem ClassTraitSystem;

    public ProjectileSystem ProjectileSystem;

    public PlayerData Data;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        EventManager<EventTypes>.Register(EventTypes.RefreshAttackSpeed, ResfreshAttackStat);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister(EventTypes.RefreshAttackSpeed, ResfreshAttackStat);
    }

    public void Init()
    {
        InitPlayerData();
        WeaponController.InitWeaponDatas();
        WeaponController.SetWeaponData(WeaponNames.WoodenSword);
        RefreshWeaponInfo();
        RefreshHP((int)MaxHP);
    }

    private void InitPlayerData()
    {
        StatSystem.AddStat(StatTID.Base, StatNames.Attack, Data.Attack);
        StatSystem.AddStat(StatTID.Base, StatNames.AttackRate, 1);
        StatSystem.AddStat(StatTID.Base, StatNames.AttackByLevel, Data.AttackByLevel);

        StatSystem.AddStat(StatTID.Base, StatNames.AttackSpeed, Data.AttackSpeed);

        StatSystem.AddStat(StatTID.Base, StatNames.CriticalChance, Data.CriticalRate);
        StatSystem.AddStat(StatTID.Base, StatNames.CriticalDamage, Data.CriticalDamage);

        StatSystem.AddStat(StatTID.Base, StatNames.Health, Data.Health);
        StatSystem.AddStat(StatTID.Base, StatNames.HealthRate, 1);
        StatSystem.AddStat(StatTID.Base, StatNames.HealthByLevel, Data.HealthByLevel);

        StatSystem.AddStat(StatTID.Base, StatNames.Armor, Data.Armor);
        StatSystem.AddStat(StatTID.Base, StatNames.ArmorRate, 1);
        StatSystem.AddStat(StatTID.Base, StatNames.ArmorByLevel, Data.ArmorByLevel);

        StatSystem.AddStat(StatTID.Base, StatNames.DodgeRate, Data.DodgeRate);

        StatSystem.AddStat(StatTID.Base, StatNames.DamageReduction, 1);
        StatSystem.AddStat(StatTID.Base, StatNames.WeaponTriggerChance, 1);
        StatSystem.AddStat(StatTID.Base, StatNames.CurrencyGainRate, 1);
        StatSystem.AddStat(StatTID.Base, StatNames.DamageRate, 1);
    }

    private void ResfreshAttackStat()
    {
        Animator.SetFloat("AttackSpeed", AttackSpeed * 2);
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

        int rand = Random.Range(0, 2);

        if (rand == 0)
        {
            Animator.SetTrigger("Attack1");
        }
        else
        {
            Animator.SetTrigger("Attack2");
        }
    }

    public override bool Hit(ref DamageInfo info)
    {
        if (base.Hit(ref info))
        {
            EventManager<EventTypes>.Send(EventTypes.PlayerDamaged);
        }

        return true;
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

        RefreshHP((int)MaxHP);

        EventManager<EventTypes>.Send(EventTypes.LevelUp, Level);
    }

    public void SkillAttack(ref DamageInfo info)
    {
        Target.Hit(ref info);
    }

    public void AnimationAttack()
    {
        if (Target == null)
        {
            Target = InGameManager.Instance.Monster;
        }

        if (Target != null)
        {
            DamageInfo info = CalculateDamage();

            Target.Hit(ref info);

            EventManager<EventTypes>.Send(EventTypes.PlayerAttackExecuted, WeaponController.Name);
        }
    }
}