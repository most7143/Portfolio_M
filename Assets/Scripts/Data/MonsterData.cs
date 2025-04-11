using UnityEngine;

[CreateAssetMenu(menuName = "Monster")]
public class MonsterData : ScriptableObject
{
    public CharacterNames Name;
    public string NameString;
    public int Level = 1;
    public int MaxLevel = 1;

    public float Attack;
    public float AttackByLevel;

    public float Health;
    public float HealthByLevel;

    public float Armor;
    public float ArmorByLevel;

    public float AttackSpeed;

#if UNITY_EDITOR

    private void OnValidate()
    {
        AssetUtility.RenameAsset(this, Name.ToString());
    }

#endif
}