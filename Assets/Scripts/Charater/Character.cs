using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterNames Name;
    [HideInInspector] public string NameString;
    public int Level = 1;
    public float MaxHp;
    public float CurrentHp;
    public float Damage = 1;
    public float AttackSpeed = 1;

    public float CriticalRage = 0.05f;
    public float CriticalDamage = 2f;

    protected float baseMaxHp;

    public bool IsAlive;

    public SpriteRenderer Renderer;

    protected virtual void Awake()
    {
        CurrentHp = MaxHp;
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

        info.IsCritical = critical <= CriticalRage;

        if (info.IsCritical)
        {
            info.Value *= CriticalDamage;
        }

        info.Value = Mathf.RoundToInt(info.Value);

        return info;
    }

    private float RandomDamage(float Damage)
    {
        float min = Damage * 0.9f;
        float max = Damage;

        return Mathf.RoundToInt(Random.Range(min, max));
    }
}