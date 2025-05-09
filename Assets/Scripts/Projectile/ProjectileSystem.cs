﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileSystem : MonoBehaviour
{
    public List<ProjectileSkill> Skills;

    private Dictionary<ProjectileNames, ProjectileSkill> _skills = new();

    private void OnEnable()
    {
        EventManager<EventTypes>.Register<WeaponNames>(EventTypes.PlayerAttackExecuted, ActivateToAttack);
        EventManager<EventTypes>.Register(EventTypes.PlayerDamaged, ActivateToDamaged);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister<WeaponNames>(EventTypes.PlayerAttackExecuted, ActivateToAttack);
        EventManager<EventTypes>.Unregister(EventTypes.PlayerDamaged, ActivateToDamaged);
    }

    public void Register(ProjectileNames name)
    {
        if (_skills.ContainsKey(name))
            return;

        for (int i = 0; i < Skills.Count; i++)
        {
            if (Skills[i].Name == name)
            {
                _skills.Add(name, Skills[i]);
                break;
            }
        }
    }

    public void ActivateToAttack(WeaponNames weapon)
    {
        if (_skills.Count == 0)
            return;

        ProjectileSkill[] skills = _skills.Values.ToArray();

        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].SkillConditions == SkillConditions.Attack)
            {
                skills[i].Shot();
            }
        }
    }

    public void ActivateToDamaged()
    {
        if (_skills.Count == 0)
            return;

        ProjectileSkill[] skills = _skills.Values.ToArray();

        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].SkillConditions == SkillConditions.Demaged)
            {
                skills[i].Shot();
            }
        }
    }

    public void Shot(ProjectileNames name)
    {
        if (_skills.ContainsKey(name))
        {
            _skills[name].Shot();
        }
    }

    public ProjectileSkill GetProjectile(ProjectileNames name)
    {
        if (_skills.ContainsKey(name))
            return _skills[name];

        return null;
    }
}