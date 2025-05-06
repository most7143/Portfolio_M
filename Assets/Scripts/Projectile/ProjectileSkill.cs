using UnityEngine;

public class ProjectileSkill : MonoBehaviour
{
    public Character Owenr;
    public ProjectileNames Name;
    public string NameString;
    public SkillConditions SkillConditions;
    public Vector3 SpawnOffsetPosition;
    public bool IsRandomSpawnPoint;
    public float Chance;
    public float DamgeRate;
    public float Speed;

    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(NameString))
        {
            Name = EXEnum.Parse<ProjectileNames>(NameString);
            gameObject.name = Name.ToString();
        }
    }

    public void Shot()
    {
        float rand = Random.Range(0, 1f);

        if (rand <= Chance)
        {
            Projectile projectile = InGameManager.Instance.ObjectPool.GetProjectile(Name);

            if (projectile != null)
            {
                Vector3 spawnPoint = SpawnOffsetPosition;
                if (IsRandomSpawnPoint)
                {
                    spawnPoint += new Vector3(Random.Range(0, 1f), Random.Range(0, 1f));
                }

                projectile.transform.position = Owenr.transform.position + spawnPoint;
                projectile.Owner = Owenr;
                projectile.DamageRate = DamgeRate;
                projectile.Speed = Speed;
                projectile.Init();
            }
        }
    }
}