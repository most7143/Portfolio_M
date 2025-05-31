using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutGameManager : MonoBehaviour
{
    private static OutGameManager instance = null;

    private Dictionary<StatNames, float> MemoryStats = new();

    private Dictionary<UIChallengeNames, ChallengeData> ChallengeDatas = new();

    public int TotalMonsterKillCount;

    public int HitOneMonterCount;

    public bool CheckNonClassChange = true;

    public bool CheckWeaponOnlySpend = true;

    public List<ClassNames> ClassChanges;

    private int targetMonsterLevel;

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
        SetChallenges();

        TotalMonsterKillCount = PlayerPrefs.GetInt("TotalMonsterKillCount");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            PlayerPrefs.DeleteAll();
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
        int point = level / 10;

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

    public ChallengeData GetChallengeData(UIChallengeNames name)
    {
        if (ChallengeDatas.ContainsKey(name))
            return ChallengeDatas[name];

        return null;
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
            if (PlayerPrefs.GetInt("WeaponOnlySpendLevel") < InGameManager.Instance.Player.Level)
            {
                PlayerPrefs.SetInt("WeaponOnlySpendLevel", InGameManager.Instance.Player.Level);
            }
        }

        if (ClassChanges.Count > 0)
        {
            for (int i = 0; i < ClassChanges.Count; i++)
            {
                PlayerPrefs.SetInt(ClassChanges[i].ToString(), 1);
            }
        }
    }
}