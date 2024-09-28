using System.Collections;
using UnityEngine;

public enum CharacterName
{
    None,
    Swordman,
    BoneWorm,
}

public class Character : MonoBehaviour
{
    public CharacterName Name;
    public int Level = 1;
    public float MaxHp;
    public float CurrentHp;
    public float Damage = 1;
    public float AttackSpeed = 1;

    public SpriteRenderer Renderer;

    private void Awake()
    {
        CurrentHp = MaxHp;
    }

    private void Start()
    {
        StartCoroutine(ProcessAttack());
    }

    public virtual void Attack()
    {
        Debug.Log(Name + ": 공격시작");
    }

    public virtual void Hit(float damage)
    {
        StartCoroutine(ProcessHitEffect());

        CurrentHp -= (int)damage;

        if (CurrentHp <= 0)
        {
            Dead();
        }
    }

    public virtual void Dead()
    {
    }

    public IEnumerator ProcessAttack()
    {
        yield return new WaitForSeconds(AttackSpeed);
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