using System.Collections.Generic;
using UnityEngine;

public class OutGameManager : MonoBehaviour
{
    private static OutGameManager instance = null;

    private Dictionary<StatNames, float> MemoryStats = new();

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static OutGameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public int MaxMemoryPoint;

    public void SetMaxMemory(int level)
    {
        int point = level / 5;

        if (MaxMemoryPoint < point)
        {
            MaxMemoryPoint = point;
        }

        if (MaxMemoryPoint > 20)
            MaxMemoryPoint = 20;

        PlayerPrefs.SetInt("MemoryMaxPoint", MaxMemoryPoint);
    }

    public void AddOutGameData(string key, int amount)
    {
        int current = PlayerPrefs.GetInt(key, 0);
        PlayerPrefs.SetInt(key, current + amount);
        PlayerPrefs.Save();
    }
}