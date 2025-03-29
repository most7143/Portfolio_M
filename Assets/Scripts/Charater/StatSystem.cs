using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SID
{
    None,
    Base,
    Item,
    Skill,
}

[Serializable]
public struct StatInfo
{
    public StatNames Name;
    public float Value;
}

public class StatSystem : MonoBehaviour
{
    public List<StatInfo> Stats;

    private Dictionary<StatNames, Dictionary<SID, float>> _stats = new();

    private void Awake()
    {
        for (int i = 0; i < Stats.Count; i++)
        {
            Dictionary<SID, float> stat = new();

            _stats.Add(Stats[i].Name, stat);

            _stats[Stats[i].Name].Add(SID.Base, Stats[i].Value);
        }
    }

    public float GetStat(StatNames name)
    {
        if (_stats.ContainsKey(name))
        {
            float result = 0;

            for (int i = 0; i < _stats[name].Count; i++)
            {
                var values = _stats[name].Values.ToArray();

                for (int j = 0; j < values.Length; j++)
                {
                    result += values[j];
                }
            }

            return result;
        }

        return -1;
    }

    public float GetStat(SID sid, StatNames name)
    {
        if (_stats.ContainsKey(name))
        {
            float result = 0;

            for (int i = 0; i < _stats[name].Count; i++)
            {
                var keys = _stats[name].Keys.ToArray();

                for (int j = 0; j < keys.Length; j++)
                {
                    if (keys[j] == sid)
                    {
                        result += _stats[name][keys[j]];
                    }
                }
            }

            return result;
        }

        return -1;
    }

    public void AddStat(SID sid, StatNames name, float value)
    {
        if (_stats.ContainsKey(name))
        {
            _stats[name].Add(sid, value);
        }
    }

    public void RemoveStat(SID sid, StatNames name)
    {
        if (_stats.ContainsKey(name))
        {
            _stats[name].Remove(sid);
        }
    }
}