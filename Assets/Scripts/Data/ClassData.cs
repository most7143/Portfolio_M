using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Class")]
public class ClassData : ScriptableObject
{
    public ClassNames Name;
    public string NameString;
    public string NameText;
    public string DescritpionText;
    public string StatDescriptionText;
    public string ProjectileDescritonText;
    public string BuffDescritonText;
    public List<StatNames> Stats;
    public List<string> StatStirng;
    public List<float> Values;

    public List<ProjectileNames> Projectiles;
    public List<string> ProjectileString;

    public List<BuffNames> BuffNames;
    public List<string> BuffString;
    public List<float> BuffAliveTimes;
    public List<float> BuffValues;

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(NameString))
        {
            Name = EXEnum.Parse<ClassNames>(NameString);
        }

        AssetUtility.RenameAsset(this, Name.ToString());

        if (ProjectileString.Count > 0)
        {
            for (int i = 0; i < ProjectileString.Count; i++)
            {
                Projectiles[i] = EXEnum.Parse<ProjectileNames>(ProjectileString[i]);
            }
        }

        if (StatStirng.Count > 0)
        {
            for (int i = 0; i < StatStirng.Count; i++)
            {
                Stats[i] = EXEnum.Parse<StatNames>(StatStirng[i]);
            }
        }

        if (BuffString.Count > 0)
        {
            for (int i = 0; i < BuffString.Count; i++)
            {
                BuffNames[i] = EXEnum.Parse<BuffNames>(BuffString[i]);
            }
        }
    }

#endif
}