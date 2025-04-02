using UnityEngine;

[CreateAssetMenu(menuName = "Passive")]
public class PassiveSkillData : ScriptableObject
{
    public PassiveSkillNames Name;
    public PassiveGrades Grade;
    public string NameString;
    public string DescriptionString;
    public int RequireRank;
    public int MaxLevel;
    public StatNames StatName;
    public StatNames BooleanStatName;
    public int Value;
    public int ValueByLevel;
    public float MultiplierValue;
    public float MultiplierValueByLevel;

#if UNITY_EDITOR

    private void OnValidate()
    {
        AssetUtility.RenameAsset(this, Name.ToString());
    }

#endif
}