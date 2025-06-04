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
    public float CurveDuration = 1f;
    public bool IsRandFlipCurve;

    public FloatyPoints FloatyPoint = FloatyPoints.Owner;
    public Vector3 FloatyPointOffset = new Vector3(-0.3f, -1.5f);
    public Animator Animator;
    public bool IsLookDirection;
    public float HitDelay = 0.1f;
    public float MultipleHit = 0;
    [HideInInspector] public float DamageRate = 1;
    [HideInInspector] public float Speed = 3;

    [SerializeField] private float arcHeight = 1.0f;

    private bool _isHit;

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

        Collider.enabled = true;
        _isHit = false;
        Shoot();
    }

    public void Shoot()
    {
        Monster monster = InGameManager.Instance.Player.Target.GetComponent<Monster>();

        if (monster != null)
        {
            if (MoveType == ProjectileMoveTypes.Curved)
            {
                StartCoroutine(ParabolaSlerpMovement(transform.position, monster.transform.position, CurveDuration));
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

    private IEnumerator ParabolaSlerpMovement(Vector3 start, Vector3 end, float time)
    {
        float timer = 0f;
        float distance = Vector2.Distance(start, end);
        float arc = distance * 0.2f; // 거리의 20%만큼만 높이로 사용

        float direction = 1f;
        if (IsRandFlipCurve)
        {
            int rand = Random.Range(0, 2);

            if (rand > 0)
            {
                direction *= -1;
            }
        }

        while (timer < time)
        {
            if (_isHit)
                break;

            float t = timer / time;
            Vector2 flat = Vector2.Lerp(start, end, t);
            float height = 4 * arc * t * (1 - t) * direction;
            flat.y += height;

            transform.position = new Vector3(flat.x, flat.y, transform.position.z);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(end.x, end.y, transform.position.z);
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
        _isHit = true;

        DamageInfo damageInfo = new();
        damageInfo.Owner = Owner;
        damageInfo.Value = (long)(Owner.Attack * DamageRate);
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