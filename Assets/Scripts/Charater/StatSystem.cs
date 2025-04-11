using System;
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
                EventManager<EventTypes>.Send(EventTypes.RefreshPlayerStst);
            }
        }
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
                    EventManager<EventTypes>.Send(EventTypes.RefreshPlayerStst);
                }
            }
        }
    }
}