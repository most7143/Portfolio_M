using UnityEngine;

public class ProjectileSkill : MonoBehaviour
{
    public Character Owenr;
    public ProjectileNames Name;
    public SkillConditions SkillConditions;

    public Vector3 SpawnOffsetPosition;
    public float Chance;
    public float DamgeRate;
    public float Speed;

    public void Shot()
    {
        float rand = Random.Range(0, 1f);

        if (rand <= Chance)
        {
            Projectile projectile = InGameManager.Instance.ObjectPool.GetProjectile(Name);

            if (projectile != null)
            {
                projectile.transform.position = Owenr.transform.position + SpawnOffsetPosition;
                projectile.Owner = Owenr;
                projectile.DamageRate = DamgeRate;
                projectile.Speed = Speed;
                projectile.Init();
            }
        }
    }
}