using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    public List<WeaponSkill> Skills;

    private Dictionary<WeaponSkillNames, WeaponSkill> _skills = new();

    private WeaponData _weaponData;

    private void OnEnable()
    {
        EventManager<EventType>.Register<WeaponNames>(EventType.AttackExecuted, ActivateToAttack);
        EventManager<EventType>.Register<WeaponNames>(EventType.EquipedWeapon, RefreshSkill);
    }

    private void OnDisable()
    {
        EventManager<EventType>.Unregister<WeaponNames>(EventType.AttackExecuted, ActivateToAttack);
        EventManager<EventType>.Unregister<WeaponNames>(EventType.EquipedWeapon, RefreshSkill);
    }

    public void RefreshSkill(WeaponNames name)
    {
        ResetSkillAlive();

        WeaponData weaponData = ResourcesManager.Instance.LoadScriptable<WeaponData>(name.ToString());

        if (weaponData.SkillNames.Count > 0)
        {
            for (int i = 0; i < weaponData.SkillNames.Count; i++)
            {
                for (int j = 0; j < Skills.Count; j++)
                {
                    if (weaponData.SkillNames[i] == Skills[j].Name)
                    {
                        _skills.Add(Skills[j].Name, Skills[j]);
                        Skills[j].Alive = true;
                    }
                }
            }
        }
    }

    private void ResetSkillAlive()
    {
        for (int i = 0; i < Skills.Count; i++)
        {
            Skills[i].Alive = false;
        }

        _skills.Clear();
    }

    public void Unregister(WeaponSkillNames name)
    {
        for (int i = 0; i < Skills.Count; i++)
        {
            if (Skills[i].Name == name)
            {
                _skills.Remove(name);
                Skills[i].Alive = false;
            }
        }
    }

    public void ActivateToAttack(WeaponNames weaponName)
    {
        if (_weaponData == null)
        {
            _weaponData = ResourcesManager.Instance.LoadScriptable<WeaponData>(weaponName.ToString());
        }

        WeaponSkillNames[] skillNames = _weaponData.SkillNames.ToArray();

        List<WeaponSkill> currentSkill = GetSkillNamesToCondition(SkillConditions.Attack);

        if (skillNames.Length > 0 && currentSkill.Count > 0)
        {
            for (int i = 0; i < skillNames.Length; i++)
            {
                _skills[skillNames[i]].Activate();
            }
        }
    }

    public List<WeaponSkill> GetSkillNamesToCondition(SkillConditions conditions)
    {
        List<WeaponSkill> skills = new List<WeaponSkill>();

        if (Skills.Count > 0)
        {
            for (int i = 0; i < Skills.Count; i++)
            {
                if (Skills[i].Condition == conditions)
                {
                    skills.Add(Skills[i]);
                }
            }
        }

        return skills;
    }
}