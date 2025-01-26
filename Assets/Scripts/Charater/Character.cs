using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterNames Name;
    public string NameString;
    public int Level = 1;
    public float MaxHp;
    public float CurrentHp;
    public float Damage = 1;
    public float AttackSpeed = 1;

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
}