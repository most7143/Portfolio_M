using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct StatInfo
{
    public StatNames Name;
    public float Value;
}

public class StatSystem : MonoBehaviour
{
    public List<StatInfo> Stats;

    private Dictionary<StatNames, float> _stats = new();

    private void Awake()
    {
        for (int i = 0; i < Stats.Count; i++)
        {
            _stats.Add(Stats[i].Name, Stats[i].Value);
        }
    }

    public float GetStat(StatNames name)
    {
        if (_stats.ContainsKey(name))
        {
            return _stats[name];
        }

        return -1;
    }

    public void SetStat(StatNames name, float value)
    {
        if (_stats.ContainsKey(name))
        {
            _stats[name] += value;
        }
    }

    private float GetStatResult(StatNames name)
    {
        if (name == StatNames.Health)
        {
            return GetStat(StatNames.Health) * GetStat(StatNames.HealthRate);
        }

        return GetStat(name);
    }
}