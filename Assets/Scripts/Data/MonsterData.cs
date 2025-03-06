using UnityEngine;

[CreateAssetMenu(menuName = "Monster")]
public class MonsterData : ScriptableObject
{
    public CharacterNames Name;
    public string NameString;
    public int Level = 1;
    public int MaxLevel = 1;
    public float AttackSpeed;

    private void OnValidate()
    {
        AssetUtility.RenameAsset(this, Name.ToString());
    }
}