using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    public Player Player;

    public List<WeaponSkill> Skills;

    private Dictionary<WeaponSkillNames, WeaponSkill> _skills = new();

    private WeaponData _weaponData;

    private void OnEnable()
    {
        EventManager<EventTypes>.Register<WeaponNames>(EventTypes.PlayerAttackExecuted, ActivateToAttack);
        EventManager<EventTypes>.Register<WeaponNames>(EventTypes.EquipedWeapon, RefreshSkill);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister<WeaponNames>(EventTypes.PlayerAttackExecuted, ActivateToAttack);
        EventManager<EventTypes>.Unregister<WeaponNames>(EventTypes.EquipedWeapon, RefreshSkill);
    }

    public void RefreshSkill(WeaponNames name)
    {
        WeaponData weaponData = ResourcesManager.Instance.LoadScriptable<WeaponData>(name.ToString());
        _weaponData = weaponData;

        if (weaponData.SkillNames.Count > 0)
        {
            for (int i = 0; i < weaponData.SkillNames.Count; i++)
            {
                for (int j = 0; j < Skills.Count; j++)
                {
                    if (weaponData.SkillNames[i] == Skills[j].Name)
                    {
                        if (false == _skills.ContainsKey(Skills[j].Name))
                        {
                            _skills.Add(Skills[j].Name, Skills[j]);
                            Skills[j].Alive = true;
                        }
                    }
                }
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
                _skills[skillNames[i]].ActivateByChance();
            }

            if (Player != null)
            {
                if (Player.StatSystem.GetStat(StatNames.RandomWeaponSkill) == 1)
                {
                    float rand = Random.Range(0, 1f);

                    if (rand >= 0.5f)
                    {
                        WeaponSkillNames anySkill = GetDifferentSkill();

                        if (anySkill != WeaponSkillNames.None)
                        {
                            _skills[anySkill].Activate();
                        }
                    }
                }
            }
        }
    }

    private WeaponSkillNames GetDifferentSkill()
    {
        List<WeaponSkillNames> weaponSkills = _weaponData.SkillNames.ToList();
        List<WeaponSkillNames> equipedSkills = _skills.Keys.ToList();

        List<WeaponSkillNames> availableSkills = equipedSkills
            .Where(skill => !weaponSkills.Contains(skill))
            .ToList();

        if (availableSkills.Count == 0)
            return WeaponSkillNames.None;

        return availableSkills[Random.Range(0, availableSkills.Count)];
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