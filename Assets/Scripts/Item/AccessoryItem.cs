using System;
using System.Collections.Generic;

public struct ValueInfo
{
    public StatNames Name;
    public float Value;
    public int Rank;
}

public class AccessoryItem
{
    public AccessoryTypes Type;
    public string NameString;
    public string DescriptionString;
    public GradeNames Grade;
    public List<ValueInfo> Values = new();

    public AccessoryData Data;

    public void ClearOption()
    {
        for (int i = 0; i < Values.Count; i++)
        {
            InGameManager.Instance.Player.StatSystem.RemoveStat((StatTID)Enum.Parse(typeof(StatTID), Type.ToString()), Values[i].Name);
        }

        Values.Clear();
        Values.Clear();
    }
}