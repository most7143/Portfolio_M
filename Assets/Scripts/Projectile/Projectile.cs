using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Character Owner;
    public Rigidbody2D Rigid;
    public Collider2D Collider;
    public ProjectileNames Name;
    public string NameString;
    public ProjectileMoveTypes MoveType;
    public float CurvedAngle = 45f;
    public Animator Animator;
    public bool IsLookDirection;
    [HideInInspector] public float DamageRate = 1;
    [HideInInspector] public float Speed = 3;

    private float hitDelay = 0.1f;

    private Vector3 launchDirection = Vector3.right + Vector3.up;

    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(NameString))
        {
            Name = EXEnum.Parse<ProjectileNames>(NameString);
            transform.name = "Projectile_" + Name.ToString();
        }
    }

    public void Init()
    {
        Rigid.bodyType = RigidbodyType2D.Dynamic;
        Collider.enabled = true;
        Shoot();
    }

    public void Shoot()
    {
        Monster monster = InGameManager.Instance.Player.TargetMonster;

        if (monster != null)
        {
            if (MoveType == ProjectileMoveTypes.Curved)
            {
                Vector3 velocity = CalculateLaunchVelocity(transform.position, monster.transform.position, Random.Range(CurvedAngle - 2, CurvedAngle + 2));
                Rigid.velocity = velocity;
                Rigid.gravityScale = 1;

                if (IsLookDirection)
                {
                    transform.right = velocity.normalized;
                }
            }
            else if (MoveType == ProjectileMoveTypes.Linear)
            {
                Vector3 direction = (monster.transform.position - transform.position).normalized;
                Rigid.velocity = direction * Speed;
                Rigid.gravityScale = 0;

                if (IsLookDirection)
                {
                    transform.right = direction.normalized;
                }
            }
        }
    }

    private Vector3 CalculateLaunchVelocity(Vector3 start, Vector3 end, float angle)
    {
        float gravity = Physics.gravity.y * -1;
        float radAngle = angle * Mathf.Deg2Rad;

        Vector3 horizontal = new Vector3(end.x - start.x, 0, end.z - start.z);
        float distance = horizontal.magnitude;
        float heightDifference = end.y - start.y;

        float v2 = (gravity * distance * distance) / (2 * (heightDifference - Mathf.Tan(radAngle) * distance) * Mathf.Pow(Mathf.Cos(radAngle), 2));
        float speed = Mathf.Sqrt(Mathf.Abs(v2));  // 속도 크기

        Vector3 direction = horizontal.normalized;
        Vector3 velocity = direction * speed * Mathf.Cos(radAngle);
        velocity.y = speed * Mathf.Sin(radAngle);

        return velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(ProcessHit());
        }
    }

    private IEnumerator ProcessHit()
    {
        yield return new WaitForSeconds(Random.Range(0f, hitDelay));
        yield return new WaitUntil(() => InGameManager.Instance.Monster != null);
        DamageInfo damageInfo = new();
        damageInfo.Owner = Owner;
        damageInfo.Value = Owner.Attack * DamageRate;
        InGameManager.Instance.Monster.Hit(ref damageInfo);
        InGameManager.Instance.ObjectPool.SpawnFloaty(transform.position + new Vector3(-0.3f, -1.5f), FloatyTypes.Damage, damageInfo.Value.ToString());
        HitAnim();
    }

    private void HitAnim()
    {
        Animator.SetTrigger("Despawn");
        Collider.enabled = false;
        Rigid.velocity = Vector3.zero;
        Rigid.bodyType = RigidbodyType2D.Kinematic;
    }

    public void Despawn()
    {
        InGameManager.Instance.ObjectPool.ReturnProjectile(Name, this);
    }
}