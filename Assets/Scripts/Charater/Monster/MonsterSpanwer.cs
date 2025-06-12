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

    public int MinEliteLevel = 1;

    public float EliteChance = 0.1f;

    public Dictionary<int, MonsterData> _datas = new();

    private void Start()
    {
        EXP = GetExp(Level);
        Gold = GetGold(Level);
        Gem = 1;

        InitDatas();
    }

    public void RefreshLevelByData(int level)
    {
        if (Level < level)
        {
            Level++;
            EXP = GetExp(Level);
            Gold = GetGold(Level);
            Gem = GetGem(Level);
        }

        float bonusSteps = ((level - 1) / 10) * 0.1f;

        SpawnMonster.StatSystem.RemoveStat(StatTID.Spawner, StatNames.AttackRate);
        SpawnMonster.StatSystem.RemoveStat(StatTID.Spawner, StatNames.ArmorRate);
        SpawnMonster.StatSystem.RemoveStat(StatTID.Spawner, StatNames.HealthRate);

        SpawnMonster.StatSystem.AddStat(StatTID.Spawner, StatNames.AttackRate, bonusSteps);
        SpawnMonster.StatSystem.AddStat(StatTID.Spawner, StatNames.ArmorRate, bonusSteps);
        SpawnMonster.StatSystem.AddStat(StatTID.Spawner, StatNames.HealthRate, bonusSteps * 3f);

        SpawnMonster.StartAttack();

        EventManager<EventTypes>.Send(EventTypes.ChangeMonsterLevel);
    }

    private int GetGold(int level)
    {
        double baseGold = 10;

        int increaseAdd = (level - 1) * 2;

        int bonusSteps = (level - 1) / 10 * 4;

        double finalGold = baseGold + increaseAdd + bonusSteps;

        return (int)Math.Round(finalGold);
    }

    private float GetExp(int level)
    {
        double baseExp = 15;

        double percentIncrease = 1 + 0.15 * (level - 1);

        int bonusSteps = (level - 1) / 10;
        double bonusMultiplier = 1 + 0.4 * bonusSteps;

        double finalExp = baseExp * percentIncrease * bonusMultiplier;

        return (int)Math.Round(finalExp);
    }

    private int GetGem(int level)
    {
        int step = level / 30;

        int minDrop = 1 + step;
        int maxDrop = 3 + step;

        return UnityEngine.Random.Range(minDrop, maxDrop + 1);
    }

    public void Spawn(CharacterNames chareacterName)
    {
        SpawnMonster.Target = InGameManager.Instance.Player;

        SpawnMonster.Name = chareacterName;
        SpawnMonster.Renderer.sprite = ResourcesManager.Instance.LoadSprite(chareacterName.ToString() + "_Idle");

        InGameManager.Instance.Player.Target = SpawnMonster;

        SpawnMonster.SetData(GetData(chareacterName));

        SpawnMonster.IsAlive = true;

        UIManager.Instance.MonsterInfo.Refresh(SpawnMonster);

        EventManager<EventTypes>.Send(EventTypes.MonsterSpawnd);
    }

    public void ChangeMonsterSkin(int level)
    {
        Spawn(GetNextMonster(level));

        UIManager.Instance.MonsterInfo.RefreshHPBar();
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