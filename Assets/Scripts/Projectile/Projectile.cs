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
    public FloatyPoints FloatyPoint = FloatyPoints.Owner;
    public Vector3 FloatyPointOffset = new Vector3(-0.3f, -1.5f);

    [HideInInspector] public float CurvedAngle = 45f;
    public Animator Animator;
    public bool IsLookDirection;
    public float HitDelay = 0.1f;
    public float MultipleHit = 0;
    [HideInInspector] public float DamageRate = 1;
    [HideInInspector] public float Speed = 3;

    private Vector3 launchDirection = Vector3.right + Vector3.up;

    private Coroutine _hitCoroutine;

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
        if (MoveType == ProjectileMoveTypes.Stop)
        {
            Rigid.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            Rigid.bodyType = RigidbodyType2D.Dynamic;
        }

        Shoot();
    }

    public void Shoot()
    {
        Monster monster = InGameManager.Instance.Player.Target.GetComponent<Monster>();

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
            if (_hitCoroutine == null)
            {
                _hitCoroutine = StartCoroutine(ProcessHit());
            }
        }
    }

    private IEnumerator ProcessHit()
    {
        yield return new WaitForSeconds(Random.Range(0f, HitDelay));

        if (MultipleHit > 0)
        {
            while (true)
            {
                Damaged();
                yield return new WaitForSeconds(MultipleHit);
            }
        }
        else
        {
            Damaged();
        }

        HitAnim();
    }

    private void Damaged()
    {
        DamageInfo damageInfo = new();
        damageInfo.Owner = Owner;
        damageInfo.Value = Owner.Attack * DamageRate;
        InGameManager.Instance.Monster.Hit(ref damageInfo);

        if (FloatyPoint == FloatyPoints.Owner)
        {
            InGameManager.Instance.ObjectPool.SpawnFloaty(transform.position + FloatyPointOffset, FloatyTypes.Damage, damageInfo.Value.ToString());
        }
        else if (FloatyPoint == FloatyPoints.Target)
        {
            InGameManager.Instance.ObjectPool.SpawnFloaty(Owner.Target.transform.position + FloatyPointOffset, FloatyTypes.Damage, damageInfo.Value.ToString());
        }
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
        StopAllCoroutines();
        _hitCoroutine = null;
    }
}