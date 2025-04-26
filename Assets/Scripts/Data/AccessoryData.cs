using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Accessory")]
public class AccessoryData : ScriptableObject
{
    public AccessoryTypes Type;
    public string[] NameStrings;
    public List<StatNames> StatNames;
    public List<float> Values;

#if UNITY_EDITOR

    private void OnValidate()
    {
        AssetUtility.RenameAsset(this, Type.ToString());
    }

#endif
}