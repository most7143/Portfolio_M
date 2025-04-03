using System.Collections.Generic;
using UnityEngine;

public class UISkillInfo : MonoBehaviour
{
    public List<PassiveSkillNames> Skills;

    public Transform Content;

    private Dictionary<PassiveSkillNames, UISkillBox> _skills = new();

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

    public void Deactivate()
    {
    }
}