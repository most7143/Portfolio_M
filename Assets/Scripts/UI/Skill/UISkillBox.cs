using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkillBox : MonoBehaviour
{
    public RectTransform Rect;
    public PassiveSkillNames Name;
    public int Level = 0;
    public int MaxLevel = 0;
    public Image Pannel;
    public Image BackGround;
    public Image Icon;
    public ImageLoop LoopImage;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescText;
    public TextMeshProUGUI BunousDescText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI CostText;
    public TextMeshProUGUI ChanceText;
    public XButton LearnButton;

    public Transform LockTrans;
    public TextMeshProUGUI LockText;

    private int _addCost;
    private int _currentCost;

    private float _chance;

    private Vector2 _origin;

    public PassiveSkillData Data;

    private void Start()
    {
        _origin = Rect.sizeDelta;
        LearnButton.OnExecute = Learn;
    }

    private void OnEnable()
    {
        EventManager<EventTypes>.Register(EventTypes.SkillLevelUp, Refresh);
        EventManager<EventTypes>.Register<ClassNames>(EventTypes.ChangeClass, RefreshChangeClass);
        EventManager<EventTypes>.Register<int>(EventTypes.LevelUp, Unlock);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister(EventTypes.SkillLevelUp, Refresh);
        EventManager<EventTypes>.Unregister<ClassNames>(EventTypes.ChangeClass, RefreshChangeClass);
        EventManager<EventTypes>.Unregister<int>(EventTypes.LevelUp, Unlock);
    }

    public void Setup(PassiveSkillNames name)
    {
        Name = name;
        Data = ResourcesManager.Instance.LoadScriptable<PassiveSkillData>(name.ToString());

        if (Data != null)
        {
            MaxLevel = Data.MaxLevel;
            _currentCost = (int)Data.Cost;
            _addCost = (int)(Data.Cost * 0.5f);
        }

        Icon.sprite = ResourcesManager.Instance.LoadSprite("Icon_" + name.ToString());

        Unlock(InGameManager.Instance.Player.Level);
        LockText.SetText(Data.RequireRank + " 랭크 도달 시 개방");
        CostText.SetText(Data.Cost + "<sprite=0>");
        Refresh();
    }

    private float GetValue()
    {
        int level = Level;

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

    public void Unlock(int level)
    {
        if (Data.RequireRank <= level)
        {
            LockTrans.gameObject.SetActive(false);
        }
    }

    public void RefreshChangeClass(ClassNames name)
    {
        Refresh();
    }

    public void Refresh()
    {
        LevelText.SetText(Level + "/" + Data.MaxLevel);

        float value = GetValue();

        if (Data.MultiplierValue > 0 && Data.StatName != StatNames.AttackSpeed)
            value = Mathf.RoundToInt(value * 100f);

        if (Level != Data.MaxLevel)
        {
            NameText.SetText(Data.NameText);
            DescText.SetText(string.Format(Data.DescriptionText, value));
            BunousDescText.gameObject.SetActive(false);
            CostText.SetText(_currentCost + "<sprite=0>");

            LoopImage.gameObject.SetActive(false);
        }
        else
        {
            Rect.sizeDelta = new Vector2(_origin.x, _origin.y + 20f);
            NameText.SetText(EXText.GetGradeColor(GradeNames.Mythic, Data.MaxLevelNameText));
            BackGround.color = Color.cyan;
            LearnButton.gameObject.SetActive(false);

            float maxLevelValue = GetMaxLevelValue();

            if (Data.MultiplierMaxLevelValue > 0 && Data.StatName != StatNames.AttackSpeed)
                maxLevelValue = Mathf.FloorToInt(maxLevelValue * 100f);

            DescText.SetText(string.Format(Data.MaxLevelDescriptionText, value, maxLevelValue));
            BunousDescText.gameObject.SetActive(true);
            BunousDescText.SetText(Data.BunousDescriptionText);
            LoopImage.gameObject.SetActive(true);

            Pannel.sprite = ResourcesManager.Instance.LoadSprite("Background_Pannel_Slice_Mythic_0");
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
            float addChance = 1 + InGameManager.Instance.Player.StatSystem.GetStat(StatNames.PassiveSkillLearnChance);

            _chance = (Data.LearnChance - (Level * Data.LearnChanceByLevel)) * addChance;

            _chance = (float)Math.Round(_chance, 2, MidpointRounding.AwayFromZero);

            if (_chance >= 1f)
            {
                _chance = 1;
            }

            ChanceText.SetText(_chance * 100f + "%");
        }
    }

    public void Learn()
    {
        if (Data.MaxLevel == Level)
            return;

        if (InGameManager.Instance.Controller.TryUsingCurrency(CurrencyTypes.Gold, _currentCost))
        {
            float rand = UnityEngine.Random.Range(0, 1f);

            if (_chance >= rand)
            {
                LevelUpSkill();

                InGameManager.Instance.ObjectPool.SpawnFloaty(LearnButton.transform.position, FloatyTypes.Success, "성공");
                EventManager<EventTypes>.Send(EventTypes.SkillLevelUp);
            }
            else
            {
                InGameManager.Instance.ObjectPool.SpawnFloaty(LearnButton.transform.position, FloatyTypes.Fail, "실패");
            }

            InGameManager.Instance.Controller.UseCurrency(CurrencyTypes.Gold, _currentCost);
        }
    }

    private void LevelUpSkill()
    {
        Level++;

        float value = GetValue();

        _currentCost += _addCost;

        Player player = InGameManager.Instance.Player;

        if (Data.StatName != StatNames.None)
        {
            player.StatSystem.RemoveStat(StatTID.PassiveSkill, Data.StatName);
            player.StatSystem.AddStat(StatTID.PassiveSkill, Data.StatName, value);
#if UNITY_EDITOR
            LogManager.LogInfo(LogTypes.Skill, string.Format("[{0}] 기술로 인해 {1} 효과가 , {2} 만큼 상승", Data.NameText, Data.StatName, value));
#endif
        }

        if (Data.BooleanStatName != StatNames.None)
        {
            player.StatSystem.RemoveStat(StatTID.PassiveSkill, Data.BooleanStatName);
            player.StatSystem.AddStat(StatTID.PassiveSkill, Data.BooleanStatName, 1);
#if UNITY_EDITOR
            LogManager.LogInfo(LogTypes.Skill, string.Format("[{0}] 기술로 인해 {1} 효과가 발동", Data.NameText, Data.BooleanStatName));
#endif
        }

        if (Level == Data.MaxLevel)
        {
            MaxLevelupSkill();
        }

        LevelUpBuff();
    }

    private void MaxLevelupSkill()
    {
#if UNITY_EDITOR
        LogManager.LogInfo(LogTypes.Skill, string.Format("[{0}] 기술의 만렙으로 인해 {1} 로 변경 ", Data.NameText, Data.MaxLevelNameText));
#endif

        if (Data.MaxLevelStatName != StatNames.None)
        {
            if (Data.MaxLevelConditions == SkillConditions.None)
            {
                InGameManager.Instance.Player.StatSystem.AddStat(StatTID.PassiveSkillMaxLevel, Data.MaxLevelStatName, GetMaxLevelValue());
#if UNITY_EDITOR
                LogManager.LogInfo(LogTypes.Skill, string.Format("[{0}] 의 스텟 {1} 부여 ", Data.MaxLevelNameText, Data.MaxLevelStatName));
#endif
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

            if (Data.Target == CharacterTypes.Player)
            {
                InGameManager.Instance.Player.BuffSystem.Register(Data.MaxLevelBuffName, Data.MaxLevelAliveTime, value);
            }
            else
            {
                InGameManager.Instance.MonsterSpanwer.SpawnMonster.BuffSystem.Register(Data.MaxLevelBuffName, Data.MaxLevelAliveTime, value);
            }
#if UNITY_EDITOR
            LogManager.LogInfo(LogTypes.Buff, string.Format("[{0}] 의 버프 {1} 부여 ", Data.MaxLevelNameText, Data.MaxLevelBuffName));
#endif
        }
    }

    private void LevelUpBuff()
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