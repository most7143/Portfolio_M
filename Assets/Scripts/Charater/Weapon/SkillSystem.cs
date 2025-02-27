using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    public List<WeaponSkill> SKills;

    private Dictionary<WeaponSkillNames, WeaponSkill> _skills;

    private void Start()
    {
        EventManager<EventType>.Register<WeaponNames>(EventType.AttackExecuted, ActivateToAttack);
    }

    public void Register(WeaponNames name)
    {
        WeaponData weaponData = ResourcesManager.Instance.LoadScriptable<WeaponData>(name.ToString());

        if (weaponData.SkillNames.Count > 0)
        {
            for (int i = 0; i < weaponData.SkillNames.Count; i++)
            {
                for (int j = 0; j < SKills.Count; j++)
                {
                    if (weaponData.SkillNames[i] == SKills[j].Name)
                    {
                        _skills.Add(SKills[j].Name, SKills[j]);
                        SKills[j].Alive = true;
                    }
                }
            }
        }
    }

    public void Unregister(WeaponSkillNames name)
    {
        for (int i = 0; i < SKills.Count; i++)
        {
            if (SKills[i].Name == name)
            {
                _skills.Remove(name);
                SKills[i].Alive = false;
            }
        }
    }

    public void ActivateToAttack(WeaponNames weaponName)
    {
        Debug.Log("스킬 사용");
        //WeaponData weapon = ResourcesManager.Instance.LoadScriptable<WeaponData>(name.ToString());

        //WeaponSkillNames[] skillNames = weapon.SkillNames.ToArray();

        //List<WeaponSkill> currentSkill = GetSkillNamesToCondition(SkillConditions.Attack);

        //if (skillNames.Length > 0 && currentSkill.Count > 0)
        //{
        //    for (int i = 0; i < skillNames.Length; i++)
        //    {
        //        _skills[skillNames[i]].Activate();
        //    }
        //}
    }

    public List<WeaponSkill> GetSkillNamesToCondition(SkillConditions conditions)
    {
        List<WeaponSkill> skills = new List<WeaponSkill>();

        if (SKills.Count > 0)
        {
            for (int i = 0; i < SKills.Count; i++)
            {
                if (SKills[i].Condition == conditions)
                {
                    skills.Add(SKills[i]);
                }
            }
        }

        return skills;
    }
}