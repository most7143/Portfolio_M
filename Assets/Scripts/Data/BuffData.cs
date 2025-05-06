using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff")]
public class BuffData : ScriptableObject
{
    [Header("Base")] public BuffNames Name;
    public string NameString;
    public string NameText;
    public string DescriptionText;

    [Header("Enum")] public BuffTypes Type;
    public ApplyTypes ApplyType;
    public BuffConditions Condition;
    public BuffConditions EndCondition;
    public BuffNames IgnoreBuffName;
    public string IgnoreBuffNameString;

    [Header("Stack")]
    public int MaxStack;

    [Header("Stat")] public List<StatNames> Stats;
    public List<string> StatsString;

    [Header("Projectile")] public List<ProjectileNames> ProjectileNames;
    public List<string> ProjectileNamesString;

    [Header("Value")] public List<float> Values;
    public float AliveTime;
    public float Cooldown;

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(NameString))
        {
            Name = EXEnum.Parse<BuffNames>(NameString);
        }

        AssetUtility.RenameAsset(this, "Buff_" + Name.ToString());

        if (!string.IsNullOrEmpty(IgnoreBuffNameString))
        {
            IgnoreBuffName = EXEnum.Parse<BuffNames>(IgnoreBuffNameString);
        }

        if (StatsString.Count > 0)
        {
            for (int i = 0; i < StatsString.Count; i++)
            {
                Stats[i] = EXEnum.Parse<StatNames>(StatsString[i]);
            }
        }

        if (ProjectileNamesString.Count > 0)
        {
            for (int i = 0; i < ProjectileNamesString.Count; i++)
            {
                ProjectileNames[i] = EXEnum.Parse<ProjectileNames>(ProjectileNamesString[i]);
            }
        }
    }

#endif
}