using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileSystem : MonoBehaviour
{
    public List<ProjectileSkill> Skills;

    private Dictionary<ProjectileNames, ProjectileSkill> _skills = new();

    private void OnEnable()
    {
        EventManager<EventTypes>.Register<WeaponNames>(EventTypes.PlayerAttackExecuted, ActivateToAttack);
        EventManager<EventTypes>.Register(EventTypes.PlayerAttackToCritical, ActivateToCriticalAttack);

        EventManager<EventTypes>.Register(EventTypes.PlayerDamaged, ActivateToDamaged);
        EventManager<EventTypes>.Register(EventTypes.UsingHeal, ActivateToHeal);
        EventManager<EventTypes>.Register<CurrencyTypes>(EventTypes.AddCurrency, ActivateToAddedGold);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister<WeaponNames>(EventTypes.PlayerAttackExecuted, ActivateToAttack);
        EventManager<EventTypes>.Unregister(EventTypes.PlayerAttackToCritical, ActivateToCriticalAttack);

        EventManager<EventTypes>.Unregister(EventTypes.PlayerDamaged, ActivateToDamaged);
        EventManager<EventTypes>.Unregister(EventTypes.UsingHeal, ActivateToHeal);
        EventManager<EventTypes>.Unregister<CurrencyTypes>(EventTypes.AddCurrency, ActivateToAddedGold);
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
        ShotToConditons(SkillConditions.Attack);
    }

    public void ActivateToCriticalAttack()
    {
        ShotToConditons(SkillConditions.CriticalAttack);
    }

    public void ActivateToDamaged()
    {
        ShotToConditons(SkillConditions.Demaged);
    }

    public void ActivateToHeal()
    {
        ShotToConditons(SkillConditions.Heal);
    }

    public void ActivateToAddedGold(CurrencyTypes type)
    {
        if (type == CurrencyTypes.Gold)
        {
            ShotToConditons(SkillConditions.AddedGold);
        }
    }

    private void ShotToConditons(SkillConditions condition)
    {
        if (_skills.Count == 0)
            return;

        ProjectileSkill[] skills = _skills.Values.ToArray();

        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].SkillConditions == condition)
            {
                if (false == skills[i].IsCooldown)
                {
                    skills[i].Shot();
                }
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