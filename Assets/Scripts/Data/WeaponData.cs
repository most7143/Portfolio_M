using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public WeaponNames Name;
    public string NameString;
    public List<WeaponSkillNames> SkillNames;
    public List<string> SkillNameString;
    public string NameText;
    public string DescText;
    public int Tier;
    public int Level;
    public int LevelByBonus;
    public float Speed;
    public float Damage;
    public float CriticalRate;
    public float CriticalDamage;
    public Sprite Icon;

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(NameString))
        {
            Name = EXEnum.Parse<WeaponNames>(NameString);
        }

        AssetUtility.RenameAsset(this, Name.ToString());

        if (SkillNameString.Count > 0)
        {
            for (int i = 0; i < SkillNameString.Count; i++)
            {
                SkillNames[i] = EXEnum.Parse<WeaponSkillNames>(SkillNameString[i]);
            }
        }
    }

#endif
}