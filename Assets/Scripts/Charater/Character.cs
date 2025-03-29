using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterNames Name;
    [HideInInspector] public string NameString;
    public int Level = 1;

    public bool IsAlive;

    public SpriteRenderer Renderer;

    public StatSystem StatSystem;

    public float CurrentHp;

    public float AttackSpeed
    { get { return StatSystem.GetStat(StatNames.AttackSpeed); } }

    public float Attack
    { get { return GetAttackStat(); } }

    public float MaxHP
    {
        get { return GetMaxHealthStat(); }
    }

    protected virtual void Awake()
    {
        IsAlive = true;
    }

    public void StartAttack()
    {
        StartCoroutine(ProcessAttack());
    }

    public virtual void OnAttack()
    {
    }

    public virtual void Hit(DamageInfo info)
    {
        StartCoroutine(ProcessHitEffect());

        info.Value = CalculateHitDamage(info.Value);

        CurrentHp -= (int)info.Value;

        if (CurrentHp <= 0)
        {
            CurrentHp = 0;

            Dead();
        }
    }

    public virtual void Dead()
    {
    }

    public IEnumerator ProcessAttack()
    {
        yield return new WaitUntil(() => InGameManager.Instance.IsBattle);
        yield return new WaitForSeconds(1f / AttackSpeed);

        OnAttack();
        StartCoroutine(ProcessAttack());
    }

    public IEnumerator ProcessHitEffect()
    {
        Renderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        Renderer.color = Color.white;
    }

    public DamageInfo CalculateDamage()
    {
        DamageInfo info = new DamageInfo();

        info.Value = RandomDamage(Attack);

        LogManager.LogInfo(LogTypes.Attack, name + " / 공격력(랜덤):" + info.Value +
            " (기본 공격력 : " + StatSystem.GetStat(SID.Base, StatNames.Attack) + " , 무기 공격력 : " + StatSystem.GetStat(SID.Item, StatNames.Attack)
            + ", 배율 : " + StatSystem.GetStat(StatNames.AttackRate));

        float critical = Random.Range(0f, 1f);

        info.IsCritical = critical <= StatSystem.GetStat(StatNames.CriticalChance);

        if (info.IsCritical)
        {
            info.Value *= StatSystem.GetStat(StatNames.CriticalDamage);
        }

        info.Value = Mathf.RoundToInt(info.Value);

        return info;
    }

    public float CalculateHitDamage(float damage)
    {
        float armor = StatSystem.GetStat(StatNames.Armor);
        float armorRate = StatSystem.GetStat(StatNames.ArmorRate);
        float reduece = StatSystem.GetStat(StatNames.DamageReduction);
        float armorByLevel = StatSystem.GetStat(StatNames.ArmorByLevel);

        float armorValue = armor + (armorByLevel * (Level - 1));

        float result = (damage - armorValue * armorRate);

        float damageMultiplier = Mathf.Clamp(2 - reduece, 0f, 1f);
        result *= damageMultiplier;

        LogManager.LogInfo(LogTypes.Damage,
   name + " /" + " 방어력(" + armorValue + "+" + (armorRate * 100f) + "%) 받는피해감소(" + (reduece - 1f) + "%) = " + result);

        return result > 0 ? result : 0;
    }

    private float RandomDamage(float Damage)
    {
        float min = Damage * 0.9f;
        float max = Damage;

        return Mathf.RoundToInt(Random.Range(min, max));
    }

    protected float GetMaxHealthStat()
    {
        float maxHp = (StatSystem.GetStat(StatNames.Health) + (StatSystem.GetStat(StatNames.HealthByLevel) * (Level - 1)))
            * StatSystem.GetStat(StatNames.HealthRate);

        return Mathf.RoundToInt(maxHp);
    }

    protected float GetAttackStat()
    {
        float attack = (StatSystem.GetStat(StatNames.Attack) + (StatSystem.GetStat(StatNames.AttackByLevel) * (Level - 1)))
             * StatSystem.GetStat(StatNames.AttackRate);

        return Mathf.RoundToInt(attack);
    }
}