using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct LogInfo
{
    public LogTypes Type;
    public bool IsUse;
}

[CreateAssetMenu(menuName = "Log")]
public class LogData : ScriptableObject
{
    public List<LogInfo> Data;
}