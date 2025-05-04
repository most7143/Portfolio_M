using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ClassTrait")]
public class ClassTraitData : ScriptableObject
{
    public ClassTraitNames Name;
    public string NameString;
    public string NameText;
    public List<ClassNames> ClassNames;
    public List<string> ClassNamesString;

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(NameString))
        {
            Name = EXEnum.Parse<ClassTraitNames>(NameString);
        }

        AssetUtility.RenameAsset(this, Name.ToString());

        if (ClassNamesString.Count > 0)
        {
            for (int i = 0; i < ClassNamesString.Count; i++)
            {
                ClassNames[i] = EXEnum.Parse<ClassNames>(ClassNamesString[i]);
            }
        }
    }

#endif
}