using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccessorySystem : MonoBehaviour
{
    private Dictionary<AccessoryTypes, AccessoryItem> items = new();

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            InitData();
        }
    }

    private void InitData()
    {
        AccessoryTypes[] types = (AccessoryTypes[])System.Enum.GetValues(typeof(AccessoryTypes));

        for (int i = 1; i < types.Length; i++)
        {
            AccessoryItem info = new();
            info.Type = types[i];
            info.NameString = i + "번 슬롯";
            info.DescriptionString = "아이템을 획득할 경우, 자동 장착";
            info.Grade = GradeNames.None;
            info.Data = ResourcesManager.Instance.LoadScriptable<AccessoryData>(types[i].ToString());
            items.Add(types[i], info);
        }
    }

    public AccessoryItem GetItem(AccessoryTypes type)
    {
        if (items.ContainsKey(type))
        {
            return items[type];
        }

        return null;
    }

    public GradeNames GetGrade(AccessoryTypes type)
    {
        if (items.ContainsKey(type))
        {
            return items[type].Grade;
        }

        return GradeNames.None;
    }

    public void Upgrade(AccessoryTypes type, GradeNames grade)
    {
        items[type].Grade = grade;

        int index = (int)grade - 1;

        items[type].NameString = items[type].Data.NameTexts[index];
        items[type].DescriptionString = items[type].Data.DescriptionTexts[index];

        ResetOption(type);
    }

    public void ResetOption(AccessoryTypes type)
    {
        AccessoryItem item = items[type];

        item.ClearOption();
        SetStats(item);
        SetValues(item);

        EventManager<EventTypes>.Send(EventTypes.RefreshAccessory);
    }

    private void SetStats(AccessoryItem item)
    {
        List<ValueInfo> Values = new();

        int count = 1;

        if (item.Grade == GradeNames.Magic || item.Grade == GradeNames.Unique)
        {
            count = 2;
        }
        else if (item.Grade == GradeNames.Legendary || item.Grade == GradeNames.Mythic)
        {
            count = 3;
        }

        for (int i = 0; i < count; i++)
        {
            float rand = Random.Range(0, 1f);

            float chance = 0;

            for (int j = 0; j < item.Data.Chance.Count; j++)
            {
                chance += item.Data.Chance[j];
                if (rand < chance)
                {
                    ValueInfo value = new();
                    value.Name = item.Data.StatNames[j];

                    Values.Add(value);
                    break;
                }
            }
        }

        item.Values = Values;
    }

    private void SetValues(AccessoryItem item)
    {
        List<ValueInfo> Values = new();

        for (int i = 0; i < item.Values.Count; i++)
        {
            StatNames stat = item.Values[i].Name;
            float chance = 0;
            int count = 0;

            float rand = Random.Range(0, 1f);

            for (int j = 0; j < item.Data.StatNames.Count; j++)
            {
                if (item.Data.StatNames[j] == stat)
                {
                    bool isSetValue = false;

                    while (false == isSetValue)
                    {
                        chance += ValueByChance(count);

                        if (rand < chance)
                        {
                            float value = GetStringToValue(item.Data.Values[j], count);

                            value = GetValueByGrade(item.Grade, value);

                            ValueInfo valueInfo = new();
                            valueInfo.Name = stat;
                            valueInfo.Value = value;
                            valueInfo.Rank = count + 1;

                            Values.Add(valueInfo);

                            InGameManager.Instance.Player.StatSystem.AddStat((StatTID)System.Enum.Parse(typeof(StatTID), item.Type.ToString()),
                            item.Data.StatNames[j], value);

                            isSetValue = true;
                        }
                        else
                        {
                            count++;
                        }
                    }

                    break;
                }
            }
        }

        item.Values = Values;
    }

    private float GetValueByGrade(GradeNames grade, float value)
    {
        if (grade == GradeNames.Rare || grade == GradeNames.Magic)
        {
            return value * 2f;
        }
        else if (grade == GradeNames.Unique || grade == GradeNames.Legendary)
        {
            return value * 3f;
        }
        else if (grade == GradeNames.Mythic)
        {
            return value * 5f;
        }

        return value;
    }

    private float ValueByChance(int count)
    {
        if (count == 0) return 0.1f;
        else if (count == 1) return 0.3f;
        else return 0.6f;
    }

    public float GetStringToValue(string valueString, int count)
    {
        List<float> values = new();

        string[] tokens = valueString.Split(',');

        for (int i = 0; i < tokens.Length; i++)
        {
            values.Add(float.Parse(tokens[i]));
        }

        return values[count];
    }
}