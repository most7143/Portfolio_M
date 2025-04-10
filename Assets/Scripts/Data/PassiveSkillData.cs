using UnityEngine;

[CreateAssetMenu(menuName = "Passive")]
public class PassiveSkillData : ScriptableObject
{
    [Header("Base")] public PassiveSkillNames Name;
    public PassiveGrades Grade;
    public string NameString;
    public string MaxLevelNameString;
    public string DescriptionString;
    public string MaxLevelDescriptionString;
    public string BunousDescriptionString;

    public int RequireRank;
    public int MaxLevel;

    [Header("Stat")] public StatNames StatName;
    public StatNames MaxLevelStatName;
    public PassiveSkillTypes MaxLevelType;
    public StatNames BooleanStatName;

    [Header("Buff")] public BuffNames BuffName;
    public BuffNames MaxLevelBuffName;
    public float AliveTime;
    public float AliveTimeByLevel;

    [Header("Value")] public int Value;
    public int ValueByLevel;
    public int MaxLevelValue;
    public float MultiplierValue;
    public float MultiplierValueByLevel;
    public float MultiplierMaxLevelValue;

    [Header("Learn")] public float LearnChance;
    public float LearnChanceByLevel;
    public float Cost;

#if UNITY_EDITOR

    private void OnValidate()
    {
        AssetUtility.RenameAsset(this, Name.ToString());
    }

#endif
}