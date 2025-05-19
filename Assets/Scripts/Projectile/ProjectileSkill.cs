using System.Collections;
using UnityEngine;

public class ProjectileSkill : MonoBehaviour
{
    public Character Owenr;
    public ProjectileNames Name;
    public string NameString;
    public SkillConditions SkillConditions;
    public int CondisionCount = 1;
    public Vector3 SpawnOffsetPosition;
    public bool IsRandomSpawnPoint;
    public float Chance;
    public float DamgeRate;
    public float Speed;
    public float Cooldown;

    public bool IsCooldown { get; private set; }
    private int currnetCount = 0;

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
        currnetCount++;

        if (currnetCount != CondisionCount)
        {
            return;
        }
        else
        {
            currnetCount = 0;
        }

        if (Cooldown > 0)
        {
            StartCoroutine(ProcessCooldown());
        }

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

    private IEnumerator ProcessCooldown()
    {
        IsCooldown = true;
        yield return new WaitForSeconds(Cooldown);
        IsCooldown = false;
    }
}