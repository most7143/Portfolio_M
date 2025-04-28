using System.Collections.Generic;
using UnityEngine;

public class AccessorySystem : MonoBehaviour
{
    private Dictionary<AccessoryTypes, AccessoryItem> items = new();

    private void Start()
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

        items[type].NameString = items[type].Data.NameStrings[index];
        items[type].DescriptionString = items[type].Data.DescriptionStrings[index];

        ResetOption(type);
    }

    public void ResetOption(AccessoryTypes type)
    {
        AccessoryItem item = items[type];

        item.ClearOption();
        item.Stats = GetStats(item);
        item.Values = GetValues(item);

        EventManager<EventTypes>.Send(EventTypes.RefreshAccessory);
    }

    private List<StatNames> GetStats(AccessoryItem item)
    {
        List<StatNames> stats = new();

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
                    stats.Add(item.Data.StatNames[j]);
                    break;
                }
            }
        }

        return stats;
    }

    private List<float> GetValues(AccessoryItem item)
    {
        List<float> values = new();

        for (int i = 0; i < item.Stats.Count; i++)
        {
            StatNames stat = item.Stats[i];
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

                            values.Add(value);

                            InGameManager.Instance.Player.StatSystem.AddStat((StatTID)System.Enum.Parse(typeof(StatTID), item.Type.ToString()),
                                item.Data.StatNames[i], value);

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

        return values;
    }

    private float ValueByChance(int count)
    {
        if (count == 1) return 0.1f;
        else if (count == 2) return 0.3f;
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