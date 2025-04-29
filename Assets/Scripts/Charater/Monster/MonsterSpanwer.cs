using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpanwer : MonoBehaviour
{
    public Monster SpawnMonster;

    public Transform SpawnPoint;

    public int Level = 1;

    public float EXP { get; private set; }

    public int Gold { get; private set; }

    public int Gem { get; private set; }

    public float AttackByLevel = 2;
    public float HealthByLevel = 20;
    public float ArmorByLevel = 1;

    public float EXPRateByLevel = 1.03f;
    public float GoldRateByLevel = 1.03f;

    public Dictionary<int, MonsterData> _datas = new();

    private void Start()
    {
        EXP = 20;
        Gold = 100;
        Gem = 1;

        InitDatas();
    }

    public void RefreshLevelByData(int level)
    {
        if (Level < level)
        {
            Level++;
            EXP *= EXPRateByLevel;
            Gold = (int)(Gold * GoldRateByLevel);
            Gem = 1 + level / 3;
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

            SpawnMonster.SetData(GetData(chareacterName));

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

    private void InitDatas()
    {
        CharacterNames[] monsters = Enum.GetValues(typeof(CharacterNames)) as CharacterNames[];

        for (int i = 1; i < monsters.Length; i++)
        {
            MonsterData monster = ResourcesManager.Instance.LoadScriptable<MonsterData>(monsters[i].ToString());

            if (monster != null)
            {
                _datas.Add(monster.Level, monster);
            }
        }
    }

    private MonsterData GetData(CharacterNames name)
    {
        MonsterData[] datas = _datas.Values.ToArray();

        for (int i = 0; i < datas.Length; i++)
        {
            if (datas[i].Name == name)
            {
                return datas[i];
            }
        }
        return null;
    }

    private CharacterNames GetNextMonster(int level)
    {
        if (_datas.ContainsKey(level))
        {
            return _datas[level].Name;
        }

        return CharacterNames.None;
    }
}