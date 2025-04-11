using UnityEngine;

[CreateAssetMenu(menuName = "Player")]
public class PlayerData : ScriptableObject
{
    public CharacterNames Name;
    public string NameString;

    public float Attack;
    public float AttackByLevel;

    public float AttackSpeed;

    public float Health;
    public float HealthByLevel;

    public float Armor;
    public float ArmorByLevel;

    public float CriticalRate;
    public float CriticalDamage;

    public float DodgeRate;

#if UNITY_EDITOR

    private void OnValidate()
    {
        AssetUtility.RenameAsset(this, Name.ToString());
    }

#endif
}