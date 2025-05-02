using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Class")]
public class ClassData : ScriptableObject
{
    public ClassNames Name;
    public string NameText;
    public string DescritpionText;
    public List<StatNames> Stats;
    public List<float> Values;

#if UNITY_EDITOR

    private void OnValidate()
    {
        AssetUtility.RenameAsset(this, Name.ToString());
    }

#endif
}