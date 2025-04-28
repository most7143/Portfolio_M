using System;
using System.Collections.Generic;

public class AccessoryItem
{
    public AccessoryTypes Type;
    public string NameString;
    public string DescriptionString;
    public GradeNames Grade;
    public List<StatNames> Stats = new();
    public List<float> Values = new();

    public AccessoryData Data;

    public void ClearOption()
    {
        for (int i = 0; i < Stats.Count; i++)
        {
            InGameManager.Instance.Player.StatSystem.RemoveStat((StatTID)Enum.Parse(typeof(StatTID), Type.ToString()), Stats[i]);
        }

        Stats.Clear();
        Values.Clear();
    }
}