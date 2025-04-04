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

    private void OnEnable()
    {
        EventManager<EventTypes>.Register(EventTypes.SkillLevelUp, Refresh);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister(EventTypes.SkillLevelUp, Refresh);
    }

    public void Setup(PassiveSkillNames name)
    {
        Name = name;
        data = ResourcesManager.Instance.LoadScriptable<PassiveSkillData>(name.ToString());
        Icon.sprite = ResourcesManager.Instance.LoadSprite("Icon_" + name.ToString());
        NameText.SetText(data.NameString);
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
            return data.MultiplierValue + ((level - 1) * data.MultiplierValueByLevel);
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

        LevelText.SetText(Level + "/" + data.MaxLevel);

        float value = GetValue();

        if (data.MultiplierValue > 0 && data.StatName != StatNames.AttackSpeed)
            value = Mathf.FloorToInt(value * 100f);

        DescText.SetText(string.Format(data.DescriptionString, value));

        if (Level != data.MaxLevel)
        {
            CostText.SetText(data.Cost + "G");
        }
        else
        {
            CostText.SetText("MAX");
        }
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
            float rand = Random.Range(0, 1f);

            if (_chance >= rand)
            {
                LevelUpSkill();
                InGameManager.Instance.Controller.UseGold((int)data.Cost);
                InGameManager.Instance.ObjectPool.SpawnFloaty(LearnButton.transform.position, FloatyTypes.Success, "성공");
                EventManager<EventTypes>.Send(EventTypes.SkillLevelUp);
            }
            else
            {
                InGameManager.Instance.ObjectPool.SpawnFloaty(LearnButton.transform.position, FloatyTypes.Fail, "실패");
            }
        }
    }

    private void LevelUpSkill()
    {
        Level++;

        float value = GetValue();

        if (data.StatName != StatNames.None)
        {
            InGameManager.Instance.Player.StatSystem.RemoveStat(StatTID.PassiveSkill, data.StatName);
            InGameManager.Instance.Player.StatSystem.AddStat(StatTID.PassiveSkill, data.StatName, value);
            LogManager.LogInfo(LogTypes.Skill, string.Format("[{0}] 기술로 인해 {1} 효과가 , {2} 만큼 상승", data.NameString, data.StatName, value));
        }

        if (data.BooleanStatName != StatNames.None)
        {
            InGameManager.Instance.Player.StatSystem.RemoveStat(StatTID.PassiveSkill, data.BooleanStatName);
            InGameManager.Instance.Player.StatSystem.AddStat(StatTID.PassiveSkill, data.BooleanStatName, 1);
            BooleanStat(data.BooleanStatName);
            LogManager.LogInfo(LogTypes.Skill, string.Format("[{0}] 기술로 인해 {1} 효과가 발동", data.NameString, data.BooleanStatName));
        }
    }

    private void BooleanStat(StatNames statName)
    {
        if (statName == StatNames.LimitHealth)
        {
            Player player = InGameManager.Instance.Player;

            if (player.CurrentHp > player.GetCurrentMaxHP())
            {
                player.RefreshHP((int)player.MaxHP);
            }
        }
    }
}