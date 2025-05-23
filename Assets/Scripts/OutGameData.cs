using UnityEngine;

public class OutGameData : MonoBehaviour
{
    private static OutGameData instance = null;

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

    public static OutGameData Instance
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

    public int CurrentMemoryPoint;

    public int MaxMemoryPoint;

    public void SetMaxMemory(int level)
    {
        int point = level / 5;

        if (MaxMemoryPoint < point)
        {
            MaxMemoryPoint = point;
        }

        PlayerPrefs.SetInt("MaxMemoryPoint", MaxMemoryPoint);
    }

    public void AddOutGameData(string key, int amount)
    {
        int current = PlayerPrefs.GetInt(key, 0);
        PlayerPrefs.SetInt(key, current + amount);
        PlayerPrefs.Save();
    }
}