using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    private Dictionary<WeaponSkillNames, WeaponSkill> SKills;

    public void Register(WeaponSkillNames Name)
    {
        WeaponSkill weaponSkill = new WeaponSkill();
    }

    public void Unregister(WeaponSkillNames Name)
    {
    }
}