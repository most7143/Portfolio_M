using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pocket")]
public class PocketData : ScriptableObject
{
    public PocketTypes Type;
    public string NameString;
    public string DescriptionString;
    public List<float> Chance;
    public List<GradeNames> Grades;
    public AccessoryTypes AccessoryType;
    public List<float> Values;
    public int UnlockRank;
    public int Cost;

#if UNITY_EDITOR

    private void OnValidate()
    {
        AssetUtility.RenameAsset(this, Type.ToString() + "Pocket");
    }

#endif
}