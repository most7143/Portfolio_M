using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Challenge")]
public class ChallengeData : ScriptableObject
{
    public UIChallengeNames Name;
    public string NameString;
    public string NameText;
    public string MaxRankNameText;
    public string DescText;
    public string MaxRankDescText;
    public string RequiredText;
    public List<float> RequireValues;
    public StatNames StatName;
    public string StatNameString;
    public List<float> StatValues;

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(NameString))
        {
            Name = EXEnum.Parse<UIChallengeNames>(NameString);
        }

        if (!string.IsNullOrEmpty(StatNameString))
        {
            StatName = EXEnum.Parse<StatNames>(StatNameString);
        }

        AssetUtility.RenameAsset(this, "Challenge_" + Name.ToString());
    }

#endif
}