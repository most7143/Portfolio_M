using UnityEngine;

[CreateAssetMenu(menuName = "Monster")]
public class MonsterData : ScriptableObject
{
    public CharacterNames Name;
    public string NameString;
    public int Level;
    public float MaxHP;
    public float MaxHPMultiplierByLevel;
    public int Damage;
    public int DamageByLevel;
    public int AttackSpeed;
}