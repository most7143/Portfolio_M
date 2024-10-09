using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public WeaponNames Name;
    public string NameString;
    public int Level;
    public int LevelByBonus;
    public float Speed;
    public float Damage;
}