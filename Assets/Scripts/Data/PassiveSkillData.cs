using UnityEngine;

[CreateAssetMenu(menuName = "Passive")]
public class PassiveSkillData : ScriptableObject
{
    [Header("Base")] public PassiveSkillNames Name;
    public string NameString;
    public PassiveGrades Grade;
    public string NameText;
    public string MaxLevelNameText;
    public string DescriptionText;
    public string MaxLevelDescriptionText;
    public string BunousDescriptionText;

    public int RequireRank;
    public int MaxLevel;

    [Header("Stat")] public StatNames StatName;
    public string StatString;
    public StatNames MaxLevelStatName;
    public string MaxLevelStatString;
    public SkillConditions MaxLevelConditions;
    public StatNames BooleanStatName;
    public string BooleanStatString;

    [Header("Buff")] public BuffNames BuffName;
    public string BuffString;
    public CharacterTypes Target = CharacterTypes.Player;
    public BuffNames MaxLevelBuffName;
    public string MaxLevelBuffString;
    public float AliveTime;
    public float AliveTimeByLevel;
    public float MaxLevelAliveTime;

    [Header("Value")] public int Value;
    public int ValueByLevel;
    public int MaxLevelValue;
    public float MultiplierValue;
    public float MultiplierValueByLevel;
    public float MultiplierMaxLevelValue;

    [Header("Learn")] public float LearnChance;
    public float LearnChanceByLevel;
    public float Cost;

    [Header("Text")] public bool IsAliveText;

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(NameString))
        {
            Name = EXEnum.Parse<PassiveSkillNames>(NameString);
        }

        if (!string.IsNullOrEmpty(StatString))
        {
            StatName = EXEnum.Parse<StatNames>(StatString);
        }

        if (!string.IsNullOrEmpty(MaxLevelStatString))
        {
            MaxLevelStatName = EXEnum.Parse<StatNames>(MaxLevelStatString);
        }

        if (!string.IsNullOrEmpty(BooleanStatString))
        {
            BooleanStatName = EXEnum.Parse<StatNames>(BooleanStatString);
        }

        if (!string.IsNullOrEmpty(BuffString))
        {
            BuffName = EXEnum.Parse<BuffNames>(BuffString);
        }

        if (!string.IsNullOrEmpty(MaxLevelBuffString))
        {
            MaxLevelBuffName = EXEnum.Parse<BuffNames>(MaxLevelBuffString);
        }

        AssetUtility.RenameAsset(this, Name.ToString());
    }

#endif
}