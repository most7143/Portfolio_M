using System;
using System.Collections.Generic;
using UnityEngine;

public struct AccessoryInfo
{
    public AccessoryTypes Type;
    public GradeTypes Grade;
    public List<StatNames> Stats;
    public List<float> Values;
}

public class AccessorySystem : MonoBehaviour
{
    private Dictionary<AccessoryTypes, AccessoryInfo> Items = new();

    private void Start()
    {
        AccessoryTypes[] types = (AccessoryTypes[])Enum.GetValues(typeof(AccessoryTypes));

        for (int i = 1; i < types.Length; i++)
        {
            AccessoryInfo info = new();
            info.Type = types[i];
            info.Grade = GradeTypes.None;
            Items.Add(types[i], info);
        }
    }

    public GradeTypes GetGrade(AccessoryTypes type)
    {
        if (Items.ContainsKey(type))
        {
            return Items[type].Grade;
        }

        return GradeTypes.None;
    }

    public void Upgrade(AccessoryTypes type, GradeTypes grade)
    {
    }
}