using System.Collections.Generic;
using UnityEngine;

public class UIPassiveSkillInfo : MonoBehaviour
{
    public List<PassiveSkillNames> Skills;

    public Transform Content;

    private Dictionary<PassiveSkillNames, UISkillBox> _skills = new();

    private void OnEnable()
    {
        EventManager<EventTypes>.Register(EventTypes.SkillLevelUp, Refresh);
        EventManager<EventTypes>.Register(EventTypes.MonsterDead, MonsterDead);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister(EventTypes.SkillLevelUp, Refresh);
        EventManager<EventTypes>.Unregister(EventTypes.MonsterDead, MonsterDead);
    }

    public void Activate()
    {
        if (_skills.Count == 0)
        {
            Init();
        }
        else
        {
            Refresh();
        }
    }

    private void Init()
    {
        for (int i = 0; i < Skills.Count; i++)
        {
            UISkillBox skillBox = ResourcesManager.Instance.Load<UISkillBox>("UISkillBox");

            if (skillBox != null)
            {
                skillBox.transform.SetParent(Content.transform);
                skillBox.Setup(Skills[i]);

                _skills.Add(Skills[i], skillBox);
            }
        }
    }

    private void Refresh()
    {
        for (int i = 0; i < Skills.Count; i++)
        {
            _skills[Skills[i]].Refresh();
        }
    }

    public UISkillBox GetPassiveSkill(PassiveSkillNames name)
    {
        if (_skills.ContainsKey(name))
        {
            return _skills[name];
        }

        return null;
    }

    public bool IsMaxSkillLevel(PassiveSkillNames name)
    {
        if (_skills.ContainsKey(name))
        {
            if (_skills[name].Level == _skills[name].MaxLevel)
            {
                return true;
            }
        }

        return false;
    }

    public void MonsterDead()
    {
        if (IsMaxSkillLevel(PassiveSkillNames.SurvivalOfTheFittest))
        {
            int stack = Mathf.FloorToInt(InGameManager.Instance.MonsterSpanwer.Level - 1 / 10);
            InGameManager.Instance.Player.StatSystem.RemoveStat(StatTID.PassiveSkillMaxLevel, _skills[PassiveSkillNames.SurvivalOfTheFittest].Data.MaxLevelStatName);
            InGameManager.Instance.Player.StatSystem.AddStat(StatTID.PassiveSkillMaxLevel, _skills[PassiveSkillNames.SurvivalOfTheFittest].Data.MaxLevelStatName,
                _skills[PassiveSkillNames.SurvivalOfTheFittest].Data.MaxLevelValue * stack);
        }
    }
}