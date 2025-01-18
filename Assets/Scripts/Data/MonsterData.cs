using UnityEngine;

[CreateAssetMenu(menuName = "Monster")]
public class MonsterData : ScriptableObject
{
    public CharacterNames Name;
    public string NameString;
    public int Level = 1;
    public int MaxLevel = 1;
    public float MaxHP;
    public float MaxHPMultiplierByLevel;
    public float Damage;
    public float DamageByLevel;
    public float AttackSpeed;
    public int EXP = 1;
    public int Gold = 1;
}