using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Memory")]
public class MemoryData : ScriptableObject
{
    public StatNames Name;
    public string NameString;
    public List<float> Values;

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(NameString))
        {
            Name = EXEnum.Parse<StatNames>(NameString);
        }

        AssetUtility.RenameAsset(this, "Memory_" + Name.ToString());
    }

#endif
}