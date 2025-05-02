using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ClassTrait")]
public class ClassTraitData : ScriptableObject
{
    public ClassTraitNames Name;
    public string NameString;
    public List<ClassNames> ClassNames;

#if UNITY_EDITOR

    private void OnValidate()
    {
        AssetUtility.RenameAsset(this, Name.ToString());
    }

#endif
}