using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutGameManager : MonoBehaviour
{
    private static OutGameManager instance = null;

    public int MaxMemoryPoint;

    public int MemoryPoint;

    public Dictionary<UIChallengeNames, ChallengeData> ChallengeDatas = new();

    public UIChallengeNames ChallengeTitleName;

    public int TotalMonsterKillCount;

    public int HitOneMonterCount;

    public bool CheckNonClassChange = true;

    public bool CheckWeaponOnlySpend = true;

    public List<ClassNames> ClassChanges;

    private int targetMonsterLevel;

    private FullScreenMode prevMode;

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

    private void Start()
    {
        string currentVersion = Application.version;
        string savedVersion = PlayerPrefs.GetString("AppVersion", "");

        if (currentVersion != savedVersion)
        {
            // 버전이 다르면, 즉 새로운 빌드일 경우
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString("AppVersion", currentVersion);
            PlayerPrefs.Save();
        }

        SetFrame();

        SetChallenges();

        TotalMonsterKillCount = PlayerPrefs.GetInt("TotalMonsterKillCount");
    }

    public void SetResolution()
    {
        float targetAspect = 9f / 16f;
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Camera camera = Camera.main;

        if (scaleHeight < 1.0f)
        {
            Rect rect = new Rect();
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            camera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = new Rect();
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            camera.rect = rect;
        }
    }

    private void SetFrame()
    {
#if UNITY_ANDROID
        Application.targetFrameRate = 60;
#endif
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

    public void SetMaxMemory(int level)
    {
        int point = level / 15;

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

    public int GetChallengeValue(UIChallengeNames name)
    {
        switch (name)
        {
            case UIChallengeNames.DiligentWorker:
                {
                    return PlayerPrefs.GetInt("TotalMonsterKillCount");
                }
            case UIChallengeNames.PocketStronghold:
                {
                    return PlayerPrefs.GetInt("HitOneMonterCount");
                }
            case UIChallengeNames.Rookie:
                {
                    return PlayerPrefs.GetInt("NonClassChangeLevel");
                }
            case UIChallengeNames.WeaponEnthusiast:
                {
                    return PlayerPrefs.GetInt("WeaponOnlySpendLevel");
                }
            case UIChallengeNames.NthReincarnation:
                {
                    int count = 0;
                    ClassNames[] names = Enum.GetValues(typeof(ClassNames)).Cast<ClassNames>().ToArray();

                    for (int i = 1; i < names.Length; i++)
                    {
                        count += PlayerPrefs.GetInt(names[i].ToString());
                    }
                    return count;
                }

            default:
                break;
        }

        return 0;
    }

    public StatNames GetChallengeStat(UIChallengeNames name)
    {
        if (ChallengeDatas.ContainsKey(name))
        {
            return ChallengeDatas[name].StatName;
        }

        return StatNames.None;
    }

    public float GetChallengeStatValue(UIChallengeNames name)
    {
        if (ChallengeDatas.ContainsKey(name))
        {
            int rank = -1;

            for (int i = 0; i <= 2; i++)
            {
                if (GetChallengeValue(name) >= ChallengeDatas[name].RequireValues[i])
                {
                    rank = i;
                }
            }

            if (rank >= 0)
            {
                return ChallengeDatas[name].StatValues[rank];
            }
        }

        return 0;
    }

    public void DamageCount(int level)
    {
        if (targetMonsterLevel == level)
        {
            HitOneMonterCount++;
        }
        else
        {
            targetMonsterLevel = level;
            HitOneMonterCount = 1;
        }
    }

    public void SetChallenges()
    {
        if (ChallengeDatas.Count > 0)
            return;

        UIChallengeNames[] names = Enum.GetValues(typeof(UIChallengeNames)).Cast<UIChallengeNames>().ToArray();

        for (int i = 1; i < names.Length; i++)
        {
            if ((int)names[i] >= 100)
                return;

            ChallengeData data = ResourcesManager.Instance.LoadScriptable<ChallengeData>("Challenge_" + names[i]);

            if (data != null)
            {
                ChallengeDatas.Add(names[i], data);

                int challengelevel = PlayerPrefs.GetInt(names[i].ToString());

                if (challengelevel < 2)
                {
                    PlayerPrefs.SetInt(names[i].ToString() + "MaxCount", (int)data.RequireValues[challengelevel]);
                }
            }
        }
    }

    public ChallengeData[] GetChallengeDatas()
    {
        if (ChallengeDatas.Count > 0)
        {
            return ChallengeDatas.Values.ToArray();
        }

        return null;
    }

    public ChallengeData GetChallengeData(UIChallengeNames name)
    {
        if (ChallengeDatas.ContainsKey(name))
            return ChallengeDatas[name];

        return null;
    }

    public void SetChallengeDataValue(UIChallengeNames name, int value)
    {
        if (name == UIChallengeNames.DiligentWorker)
        {
            TotalMonsterKillCount = value;
        }
        else if (name == UIChallengeNames.PocketStronghold)
        {
            PlayerPrefs.SetInt("HitOneMonterCount", value);
        }
        else if (name == UIChallengeNames.Rookie)
        {
            PlayerPrefs.SetInt("NonClassChangeLevel", value);
        }
        else if (name == UIChallengeNames.WeaponEnthusiast)
        {
            PlayerPrefs.SetInt("WeaponOnlySpendLevel", value);
        }
        else if (name == UIChallengeNames.NthReincarnation)
        {
            ClassNames[] names = Enum.GetValues(typeof(ClassNames)).Cast<ClassNames>().ToArray();

            for (int i = 1; i < names.Length; i++)
            {
                PlayerPrefs.SetInt(names[i].ToString(), 1);
            }
        }
    }

    public void SaveChallengeData()
    {
        PlayerPrefs.SetInt("TotalMonsterKillCount", TotalMonsterKillCount);

        if (PlayerPrefs.GetInt("HitOneMonterCount") < HitOneMonterCount)
        {
            PlayerPrefs.SetInt("HitOneMonterCount", HitOneMonterCount);
        }

        if (CheckNonClassChange)
        {
            if (PlayerPrefs.GetInt("NonClassChangeLevel") < InGameManager.Instance.Player.Level)
            {
                PlayerPrefs.SetInt("NonClassChangeLevel", InGameManager.Instance.Player.Level);
            }
        }

        if (CheckWeaponOnlySpend)
        {
            if (PlayerPrefs.GetInt("WeaponOnlySpendLevel") < InGameManager.Instance.Monster.Level)
            {
                PlayerPrefs.SetInt("WeaponOnlySpendLevel", InGameManager.Instance.Monster.Level);
            }
        }

        if (ClassChanges.Count > 0)
        {
            for (int i = 0; i < ClassChanges.Count; i++)
            {
                PlayerPrefs.SetInt(ClassChanges[i].ToString(), 1);
            }
        }

        ChallengeTitleName = GetAllClearByTitle();
    }

    public UIChallengeNames GetAllClearByTitle()
    {
        UIChallengeNames resultName = UIChallengeNames.None;

        bool isSet = false;

        for (int i = 2; i >= 0; i--)
        {
            UIChallengeNames[] names = { UIChallengeNames.TraceOfTheFirstMark, UIChallengeNames.FootprintsLeftBehind, UIChallengeNames.RemnantsOfWillpower };
            UIChallengeNames[] challenges = ChallengeDatas.Keys.ToArray();

            for (int j = 0; j < ChallengeDatas.Count; j++)
            {
                ChallengeData data = GetChallengeData(challenges[i]);
                if (GetChallengeValue(challenges[i]) < data.RequireValues[i])
                {
                    resultName = UIChallengeNames.None;
                    break;
                }

                if (j == ChallengeDatas.Count - 1)
                {
                    resultName = names[i];
                    isSet = true;
                }
            }

            if (isSet)
                break;
        }

        return resultName;
    }
}