using UnityEngine;

public static class LogManager
{
    private static LogData data;

#if UNITY_EDITOR

    public static void LogInfo(LogTypes type, string text)
    {
        if (data == null)
        {
            data = ResourcesManager.Instance.LoadScriptable<LogData>("LogData");
        }

        for (int i = 0; i < data.Data.Count; i++)
        {
            if (data.Data[i].Type == type
                && data.Data[i].IsUse)
            {
                Debug.Log("[" + type.ToString() + "] : " + text);
            }
        }
    }

#endif
}