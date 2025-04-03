using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkillBox : MonoBehaviour
{
    public PassiveSkillNames Name;
    public int Level = 0;
    public Image Icon;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI CostText;
    public TextMeshProUGUI ChanceText;
    public Button LearnButton;

    public Transform LockTrans;
    public TextMeshProUGUI LockText;

    private float _chance;

    private PassiveSkillData data;

    private void Start()
    {
        LearnButton.onClick.AddListener(() => Learn());
    }

    public void Setup(PassiveSkillNames name)
    {
        Name = name;
        data = ResourcesManager.Instance.LoadScriptable<PassiveSkillData>(name.ToString());
        Icon.sprite = ResourcesManager.Instance.LoadSprite("Icon_" + name.ToString());
        NameText.SetText(data.NameString);
        DescText.SetText(string.Format(data.DescriptionString, GetValue()));
        LevelText.SetText(Level + "/" + data.MaxLevel);
        LockText.SetText(data.RequireRank + " 랭크 도달 시 개방");
        CostText.SetText(data.Cost + "G");
        Refresh();
    }

    private float GetValue()
    {
        int level = Level;

        if (Level < 1)
            level = 1;

        if (data.Value != 0)
        {
            return data.Value + ((level - 1) * data.ValueByLevel);
        }
        else if (data.MultiplierValue != 0)
        {
            float value = data.MultiplierValue + ((level - 1) * data.MultiplierValueByLevel);

            if (data.StatName != StatNames.AttackSpeed)
            {
                value *= 100;
            }

            return value;
        }

        return 0;
    }

    public void Refresh()
    {
        if (InGameManager.Instance.Player.Level >= data.RequireRank)
        {
            LockTrans.gameObject.SetActive(false);
        }
        else
        {
            LockTrans.gameObject.SetActive(true);
        }

        SetChance();
    }

    private void SetChance()
    {
        _chance = data.LearnChance - (Level * data.LearnChanceByLevel);

        ChanceText.SetText(_chance * 100f + "%");
    }

    public void Learn()
    {
        if (data.MaxLevel == Level)
            return;

        if (InGameManager.Instance.Controller.TryUsingGold((int)data.Cost))
        {
            float rand = Random.Range(0, 1);

            if (_chance >= rand)
            {
                LevelUpSkill();
                InGameManager.Instance.Controller.UseGold((int)data.Cost);
                InGameManager.Instance.ObjectPool.SpawnFloaty(LearnButton.transform.position, FloatyTypes.Success, "성공");
            }
            else
            {
                InGameManager.Instance.ObjectPool.SpawnFloaty(LearnButton.transform.position, FloatyTypes.Success, "실패");
            }
        }
    }

    private void LevelUpSkill()
    {
        Level++;

        float value = GetValue();

        if (data.StatName != StatNames.None)
        {
            InGameManager.Instance.Player.StatSystem.AddStat(StatTID.PassiveSkill, data.StatName, value);
            LogManager.LogInfo(LogTypes.Skill, string.Format("[{0}] 기술로 인해 {1} 효과가 , {2} 만큼 상승", Name, data.StatName, value));
        }

        if (data.BooleanStatName != StatNames.None)
        {
            InGameManager.Instance.Player.StatSystem.AddStat(StatTID.PassiveSkill, data.BooleanStatName, 1);
            LogManager.LogInfo(LogTypes.Skill, string.Format("[{0}] 기술로 인해 {1} 효과가 발동", Name, data.BooleanStatName));
        }
    }
}