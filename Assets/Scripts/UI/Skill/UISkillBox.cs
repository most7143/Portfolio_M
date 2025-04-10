using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkillBox : MonoBehaviour
{
    public RectTransform Rect;
    public PassiveSkillNames Name;
    public int Level = 0;
    public int MaxLevel = 0;
    public Image BackGround;
    public Image Icon;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescText;
    public TextMeshProUGUI BunousDescText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI CostText;
    public TextMeshProUGUI ChanceText;
    public Button LearnButton;

    public Transform LockTrans;
    public TextMeshProUGUI LockText;

    private float _chance;

    private Vector2 _origin;

    public PassiveSkillData Data;

    private void Start()
    {
        _origin = Rect.sizeDelta;
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
        Data = ResourcesManager.Instance.LoadScriptable<PassiveSkillData>(name.ToString());

        if (Data != null)
        {
            MaxLevel = Data.MaxLevel;
        }

        Icon.sprite = ResourcesManager.Instance.LoadSprite("Icon_" + name.ToString());

        LockText.SetText(Data.RequireRank + " 랭크 도달 시 개방");
        CostText.SetText(Data.Cost + "G");
        Refresh();
    }

    private float GetValue()
    {
        int level = Level;

        if (Level < 1)
            level = 1;

        if (Data.Value != 0)
        {
            return Data.Value + ((level - 1) * Data.ValueByLevel);
        }
        else if (Data.MultiplierValue != 0)
        {
            return Data.MultiplierValue + ((level - 1) * Data.MultiplierValueByLevel);
        }

        return 0;
    }

    private float GetMaxLevelValue()
    {
        float value = 0;

        if (Data.MaxLevelValue > 0)
        {
            value = Data.MaxLevelValue;
        }
        else if (Data.MultiplierMaxLevelValue > 0)
        {
            value = Data.MultiplierMaxLevelValue;
        }

        return value;
    }

    public void Refresh()
    {
        if (InGameManager.Instance.Player.Level >= Data.RequireRank)
        {
            LockTrans.gameObject.SetActive(false);
        }
        else
        {
            LockTrans.gameObject.SetActive(true);
        }

        LevelText.SetText(Level + "/" + Data.MaxLevel);

        float value = GetValue();

        if (Data.MultiplierValue > 0 && Data.StatName != StatNames.AttackSpeed)
            value = Mathf.RoundToInt(value * 100f);

        if (Level != Data.MaxLevel)
        {
            NameText.SetText(Data.NameString);
            DescText.SetText(string.Format(Data.DescriptionString, value));
            BunousDescText.gameObject.SetActive(false);
            CostText.SetText(Data.Cost + "G");
        }
        else
        {
            Rect.sizeDelta = new Vector2(_origin.x, _origin.y + 20f);
            NameText.SetText("<color=#96FFFF>" + Data.MaxLevelNameString + "</color>");
            BackGround.color = Color.cyan;

            float maxLevelValue = GetMaxLevelValue();

            if (Data.MultiplierMaxLevelValue > 0 && Data.StatName != StatNames.AttackSpeed)
                maxLevelValue = Mathf.FloorToInt(maxLevelValue * 100f);

            DescText.SetText(string.Format(Data.MaxLevelDescriptionString, value, maxLevelValue));
            BunousDescText.gameObject.SetActive(true);
            BunousDescText.color = Color.cyan;
            BunousDescText.SetText(Data.BunousDescriptionString);
            CostText.SetText("MAX");
        }

        SetChance();
    }

    private void SetChance()
    {
        if (Level == Data.MaxLevel)
        {
            ChanceText.gameObject.SetActive(false);
        }
        else
        {
            _chance = Data.LearnChance - (Level * Data.LearnChanceByLevel);
            ChanceText.SetText(_chance * 100f + "%");
        }
    }

    public void Learn()
    {
        if (Data.MaxLevel == Level)
            return;

        if (InGameManager.Instance.Controller.TryUsingGold((int)Data.Cost))
        {
            float rand = Random.Range(0, 1f);

            if (_chance >= rand)
            {
                LevelUpSkill();
                InGameManager.Instance.Controller.UseGold((int)Data.Cost);
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

        Player player = InGameManager.Instance.Player;

        if (Data.StatName != StatNames.None)
        {
            player.StatSystem.RemoveStat(StatTID.PassiveSkill, Data.StatName);
            player.StatSystem.AddStat(StatTID.PassiveSkill, Data.StatName, value);
            LogManager.LogInfo(LogTypes.Skill, string.Format("[{0}] 기술로 인해 {1} 효과가 , {2} 만큼 상승", Data.NameString, Data.StatName, value));
        }

        if (Data.BooleanStatName != StatNames.None)
        {
            player.StatSystem.RemoveStat(StatTID.PassiveSkill, Data.BooleanStatName);
            player.StatSystem.AddStat(StatTID.PassiveSkill, Data.BooleanStatName, 1);
            BooleanStat(Data.BooleanStatName);
            LogManager.LogInfo(LogTypes.Skill, string.Format("[{0}] 기술로 인해 {1} 효과가 발동", Data.NameString, Data.BooleanStatName));
        }

        if (Level == Data.MaxLevel)
        {
            MaxLevelupSkill();
        }

        LevleUpBuff();
    }

    private void MaxLevelupSkill()
    {
        if (Data.MaxLevelStatName != StatNames.None)
        {
            if (Data.MaxLevelType != PassiveSkillTypes.Stack)
            {
                InGameManager.Instance.Player.StatSystem.AddStat(StatTID.PassiveSkillMaxLevel, Data.MaxLevelStatName, GetMaxLevelValue());
                LogManager.LogInfo(LogTypes.Skill, string.Format("[{0}] 기술의 만렙으로 인해 {1} 로 변경 ", Data.NameString, Data.MaxLevelNameString));
            }
        }
        else if (Data.MaxLevelBuffName != BuffNames.None)
        {
            float value = 0;

            if (Data.MaxLevelValue > 0)
            {
                value = Data.MaxLevelValue;
            }
            else if (Data.MultiplierMaxLevelValue > 0)
            {
                value = Data.MultiplierMaxLevelValue;
            }

            InGameManager.Instance.Player.BuffSystem.Register(Data.MaxLevelBuffName, Data.AliveTime, value);
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

    private void LevleUpBuff()
    {
        int level = Level;

        if (Level < 1)
            level = 1;

        Player player = InGameManager.Instance.Player;

        if (Data.BuffName != BuffNames.None)
        {
            float aliveTime = Data.AliveTime + ((level - 1) * Data.AliveTimeByLevel);
            player.BuffSystem.Register(Data.BuffName, aliveTime, GetValue());
        }
    }
}