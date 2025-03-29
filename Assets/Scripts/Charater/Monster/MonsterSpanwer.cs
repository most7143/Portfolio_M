using System;
using UnityEngine;

public class MonsterSpanwer : MonoBehaviour
{
    public Monster SpawnMonster;

    public Transform SpawnPoint;

    public int Level = 1;

    [HideInInspector] public float EXP = 10;
    [HideInInspector] public int Gold = 5;

    public void RefreshLevelByData(int level)
    {
        if (Level < level)
        {
            Level++;
            EXP *= 1.02f;
            Gold = (int)(Gold * 1.03f);
        }
    }

    public void Spawn(CharacterNames chareacterName)
    {
        GameObject monster = ResourcesManager.Instance.Load(chareacterName);
        if (monster != null)
        {
            SpawnMonster = monster.GetComponent<Monster>();

            SpawnMonster.Spanwer = this;
            SpawnMonster.SetData();

            SpawnMonster.transform.SetParent(SpawnPoint);
            SpawnMonster.transform.localPosition = Vector3.zero;

            SpawnMonster.IsAlive = true;

            UIManager.Instance.MonsterInfo.Refresh(SpawnMonster);

            SpawnMonster.StartAttack();
        }
    }

    public void Respawn(int level)
    {
        Deswpawn();

        Spawn(GetNextMonster(level));
    }

    public void Deswpawn()
    {
        if (SpawnMonster != null)
        {
            Destroy(SpawnMonster.gameObject);
        }
    }

    private CharacterNames GetNextMonster(int level)
    {
        CharacterNames[] monsters = Enum.GetValues(typeof(CharacterNames)) as CharacterNames[];

        for (int i = 1; i < monsters.Length; i++)
        {
            MonsterData monster = ResourcesManager.Instance.LoadScriptable<MonsterData>(monsters[i].ToString());

            if (monster != null)
            {
                if (monster.Level == level)
                {
                    return monster.Name;
                }
            }
        }

        return CharacterNames.None;
    }
}