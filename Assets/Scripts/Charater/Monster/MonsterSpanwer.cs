using System;
using UnityEngine;

public class MonsterSpanwer : MonoBehaviour
{
    public Monster SpawnMonster;

    public Transform SpawnPoint;

    public int Level = 1;

    public float EXP { get; private set; }

    public int Gold { get; private set; }

    public float AttackByLevel = 2;
    public float HealthByLevel = 20;
    public float ArmorByLevel = 1;

    public float EXPRateByLevel = 1.03f;
    public float GoldRateByLevel = 1.03f;

    private void Start()
    {
        EXP = 20;
        Gold = 100;
    }

    public void RefreshLevelByData(int level)
    {
        if (Level < level)
        {
            Level++;
            EXP *= EXPRateByLevel;
            Gold = (int)(Gold * GoldRateByLevel);
        }

        EventManager<EventTypes>.Send(EventTypes.ChangeMonsterLevel);
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

            EventManager<EventTypes>.Send(EventTypes.MonsterSpawnd);
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