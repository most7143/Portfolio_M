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

    public float EXPRateByLevel = 1.03f;
    public float BaseEXP = 10;
    public float BaseGold = 100;
    public float GoldRateByLevel = 1.03f;

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
            EXP *= EXPRateByLevel;
            Gold = GetGold(level);
            Gem = 1 + level / 3;
        }

        EventManager<EventTypes>.Send(EventTypes.ChangeMonsterLevel);
    }

    private int GetGold(int level)
    {
        return Mathf.CeilToInt(BaseGold * (level * GoldRateByLevel));
    }

    private float GetExp(int level)
    {
        return Mathf.CeilToInt(BaseEXP * (level * EXPRateByLevel));
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

        SpawnMonster.StartAttack();

        EventManager<EventTypes>.Send(EventTypes.MonsterSpawnd);
    }

    public void ChangeMonsterSkin(int level)
    {
        Spawn(GetNextMonster(level));
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