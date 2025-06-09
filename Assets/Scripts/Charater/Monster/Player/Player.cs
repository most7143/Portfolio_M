using System.Linq;
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

    private CharacterNames _currentTargetMonster;

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
        InitMemoryData();
        InitChallengeData();
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
        StatSystem.AddStat(StatTID.Base, StatNames.ExpGainRate, 1);
        StatSystem.AddStat(StatTID.Base, StatNames.DamageRate, 1);
        StatSystem.AddStat(StatTID.Base, StatNames.WeaponSkillDamageRate, 1);
    }

    private void InitMemoryData()
    {
        StatNames[] stats = { StatNames.Attack, StatNames.Armor, StatNames.Health, StatNames.AttackSpeed,StatNames.CriticalChance,
        StatNames.CriticalDamage,StatNames.AllStats,StatNames.CurrencyGainRate,StatNames.ExpGainRate};

        for (int i = 0; i < stats.Length; i++)
        {
            int memoryPoint = PlayerPrefs.GetInt("Memory" + stats[i].ToString());

            if (memoryPoint == 0)
                continue;

            int point = (memoryPoint - 1) % 3;

            MemoryData data = ResourcesManager.Instance.LoadScriptable<MemoryData>("Memory_" + stats[i].ToString());
            if (data != null)
            {
                StatSystem.AddStat(StatTID.Memory, stats[i], data.Values[point]);
            }
        }
    }

    private void InitChallengeData()
    {
        UIChallengeNames[] names = OutGameManager.Instance.ChallengeDatas.Keys.ToArray();

        for (int i = 0; i < names.Length; i++)
        {
            StatNames statName = OutGameManager.Instance.GetChallengeStat(names[i]);

            float value = OutGameManager.Instance.GetChallengeStatValue(names[i]);

            if (value > 0)
            {
                StatSystem.AddStat(StatTID.Challenge, statName, value);
            }
        }

        if (OutGameManager.Instance.ChallengeTitleName != UIChallengeNames.None)
        {
            ChallengeData data = ResourcesManager.Instance.LoadScriptable<ChallengeData>("Challenge_" + OutGameManager.Instance.ChallengeTitleName);
            if (data != null)
            {
                StatSystem.AddStat(StatTID.Challenge, data.StatName, data.StatValues[0]);
            }
        }
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

        int rand = UnityEngine.Random.Range(0, 2);

        if (rand == 0)
        {
            Animator.SetTrigger("Attack1");
            SoundManager.Instance.Play(SoundNames.Slash1);
        }
        else
        {
            Animator.SetTrigger("Attack2");
            SoundManager.Instance.Play(SoundNames.Slash2);
        }
    }

    public override bool Hit(ref DamageInfo info)
    {
        if (base.Hit(ref info))
        {
            OutGameManager.Instance.DamageCount(info.Owner.Level);

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

        SoundManager.Instance.Play(SoundNames.LevelUp);

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