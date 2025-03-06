using UnityEngine;

public class MonsterSpanwer : MonoBehaviour
{
    public Monster SpawnMonster;

    public Transform SpawnPoint;

    public int Level = 1;

    [HideInInspector] public float Damage;
    [HideInInspector] public float MaxHP;

    public float DamageByLevel = 1.05f;
    public float MaxHPMultiplierByLevel = 1.2f;

    [HideInInspector] public float EXP = 10;
    [HideInInspector] public int Gold = 5;

    public void RefreshLevelByData(int level)
    {
        if (Level < level)
        {
            Level++;
            EXP *= 1.1f;
            Gold = (int)(Gold * 1.2f);
        }
    }

    public void Spawn(CharacterNames chareacterName)
    {
        GameObject monster = ResourcesManager.Instance.Load(chareacterName);
        if (monster != null)
        {
            SpawnMonster = monster.GetComponent<Monster>();

            SpawnMonster.Spanwer = this;
            SpawnMonster.SetData(Damage, MaxHP);

            SpawnMonster.transform.SetParent(SpawnPoint);
            SpawnMonster.transform.localPosition = Vector3.zero;

            SpawnMonster.IsAlive = true;

            UIManager.Instance.MonsterInfo.Refresh(SpawnMonster);

            SpawnMonster.StartAttack();

            Damage = SpawnMonster.Damage;
            MaxHP = SpawnMonster.MaxHp;
        }
    }

    public void Respawn(CharacterNames chareacterName)
    {
        Deswpawn();
        Spawn(chareacterName);
    }

    public void Deswpawn()
    {
        if (SpawnMonster != null)
        {
            Destroy(SpawnMonster.gameObject);
        }
    }
}