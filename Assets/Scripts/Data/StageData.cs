using UnityEngine;

[CreateAssetMenu(menuName = "Stage")]
public class StageData : ScriptableObject
{
    public StageNames Name;
    public string NameString;
    public int Level = 1;
}