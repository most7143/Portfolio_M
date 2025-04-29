using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct StatInfo
{
    public StatNames Name;
    public float Value;
}

public class StatSystem : MonoBehaviour
{
    public Character Owenr;

    private Dictionary<StatNames, Dictionary<StatTID, float>> _stats = new();

    public bool IsInvincibility = false;

    private void Awake()
    {
        if (Owenr == null)
        {
            Owenr = this.GetComponent<Character>();
        }
    }

    public float GetStat(StatNames name)
    {
        if (_stats.ContainsKey(name))
        {
            float result = 0;

            var values = _stats[name].Values.ToArray();

            for (int j = 0; j < values.Length; j++)
            {
                result += values[j];
            }

            return result;
        }

        return 0;
    }

    public float GetStat(StatTID tid, StatNames name)
    {
        if (_stats.ContainsKey(name))
        {
            float result = 0;

            var keys = _stats[name].Keys.ToArray();

            for (int j = 0; j < keys.Length; j++)
            {
                if (keys[j] == tid)
                {
                    result += _stats[name][keys[j]];
                }
            }

            return result;
        }

        return 0;
    }

    public void AddStat(StatTID tid, StatNames name, float value)
    {
        if (false == _stats.ContainsKey(name))
        {
            _stats.Add(name, new Dictionary<StatTID, float>());
        }

        if (_stats[name].ContainsKey(tid))
        {
            _stats[name][tid] += value;
        }
        else
        {
            _stats[name].Add(tid, value);
        }

        if (Owenr != null)
        {
            if (Owenr.Name == CharacterNames.Swordman)
            {
                EventManager<EventTypes>.Send(EventTypes.RefreshPlayerStst, name);
            }

            if (name == StatNames.AttackSpeed || name == StatNames.DoubleAttackSpeed)
            {
                EventManager<EventTypes>.Send(EventTypes.RefreshAttackSpeed);
            }

            StatTrigger(name);
        }
#if UNITY_EDITOR

        LogManager.LogInfo(LogTypes.Stat, string.Format("{0} / {1} = {2} 이 추가됩니다. ({3}) ", Owenr.Name.ToString(), name, value, tid.ToString()));
#endif
    }

    public void RemoveStat(StatTID tid, StatNames name)
    {
        if (_stats.ContainsKey(name))
        {
            _stats[name].Remove(tid);

            if (Owenr != null)
            {
                if (Owenr.Name == CharacterNames.Swordman)
                {
                    EventManager<EventTypes>.Send(EventTypes.RefreshPlayerStst, name);
                }
            }

#if UNITY_EDITOR

            LogManager.LogInfo(LogTypes.Stat, string.Format("{0} / {1} 이 제거됩니다. ({2}) ", Owenr.Name.ToString(), name, tid.ToString()));
#endif
        }
    }

    private void StatTrigger(StatNames name)
    {
        if (name == StatNames.LimitHealth)
        {
            if (GetStat(StatNames.LimitHealth) == 1)
            {
                if (Owenr.CurrentHp > Owenr.GetCurrentMaxHP())
                {
                    Owenr.RefreshHP((int)Owenr.MaxHP);
                }
            }
        }
    }
}