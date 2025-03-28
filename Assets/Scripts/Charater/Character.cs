using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterNames Name;
    [HideInInspector] public string NameString;
    public int Level = 1;
    public float Damage = 1;

    public float AttackSpeed = 1;

    protected float baseMaxHp;

    public bool IsAlive;

    public SpriteRenderer Renderer;

    public StatSystem StatSystem;

    public float CurrentHp;

    public float MaxHp
    {
        get { return StatSystem.GetStat(StatNames.Health) * StatSystem.GetStat(StatNames.HealthRate); }
        set { StatSystem.SetStat(StatNames.Health, value); }
    }

    protected virtual void Awake()
    {
        IsAlive = true;
    }

    public void StartAttack()
    {
        StartCoroutine(ProcessAttack());
    }

    public virtual void Attack()
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

        Attack();
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

        info.Value = RandomDamage(Damage);

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

        float result = (damage - armor * armorRate);

        float damageMultiplier = Mathf.Clamp(2 - reduece, 0f, 1f);
        result *= damageMultiplier;

        LogManager.LogInfo(LogTypes.Damage,
  "(" + Name + ") :" + " 방어력(" + armor + "+" + (armorRate * 100f) + "%) 받는피해감소(" + (reduece - 1f) + "%) = " + result);

        return result > 0 ? result : 0;
    }

    private float RandomDamage(float Damage)
    {
        float min = Damage * 0.9f;
        float max = Damage;

        return Mathf.RoundToInt(Random.Range(min, max));
    }
}