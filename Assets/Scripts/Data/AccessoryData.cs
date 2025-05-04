using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Accessory")]
public class AccessoryData : ScriptableObject
{
    public AccessoryTypes Type;
    public string[] NameTexts;
    public string[] DescriptionTexts;
    public List<StatNames> StatNames;
    public List<string> StatString;
    public List<float> Chance;
    public List<string> Values;

#if UNITY_EDITOR

    private void OnValidate()
    {
        AssetUtility.RenameAsset(this, Type.ToString());

        if (StatString.Count > 0)
        {
            for (int i = 0; i < StatString.Count; i++)
            {
                StatNames[i] = EXEnum.Parse<StatNames>(StatString[i]);
            }
        }
    }

#endif
}