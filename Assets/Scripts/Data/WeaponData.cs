using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public WeaponNames Name;
    public FXNames FXName;
    public string NameString;
    public string DescString;
    public int Tier;
    public int Level;
    public int LevelByBonus;
    public float Speed;
    public float Damage;
    public float CriticalRate;
    public float CriticalDamage;
    public Sprite Icon;
}