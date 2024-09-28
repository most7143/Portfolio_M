using UnityEngine;

[CreateAssetMenu(menuName = "Monster")]
public class MonsterData : ScriptableObject
{
    public CharacterName Name;
    public int Level;
    public float MaxHP;
    public float MaxHPMultiplierByLevel;
    public int Damage;
    public int DamageByLevel;
}