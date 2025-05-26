using UnityEngine;

[CreateAssetMenu(menuName = "Buff")]
public class BuffData : ScriptableObject
{
    [Header("Base")] public BuffNames Name;
    public string NameString;
    public string NameText;

    [Header("Enum")] public BuffTypes Type;
    public ApplyTypes ApplyType;

    [Header("Stack")]
    public int MaxStack;

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(NameString))
        {
            Name = EXEnum.Parse<BuffNames>(NameString);
        }

        AssetUtility.RenameAsset(this, "Buff_" + Name.ToString());
    }

#endif
}