using System;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponInfo
{
    public WeaponNames Name;
    public WeaponSkillNames[] SkillNames;
    public string NameText;
    public string DescText;
    public int Tier;
    public int Level;
    public float Speed;
    public float Damage;
    public float CriticalRate;
    public float CriticalDamage;
    public Sprite Icon;
}

public class WeaponController : MonoBehaviour
{
    public WeaponNames Name
    { get { return Info.Name; } }

    public WeaponInfo Info;

    private WeaponData currentData;

    private List<WeaponData> datas = new();

    public void InitWeaponDatas()
    {
        WeaponNames[] weaponNames = (WeaponNames[])Enum.GetValues(typeof(WeaponNames));
        for (int i = 1; i < weaponNames.Length; i++)
        {
            WeaponData weaponData = ResourcesManager.Instance.LoadScriptable<WeaponData>(weaponNames[i].ToString());

            if (weaponData != null)
            {
                datas.Add(weaponData);
            }
        }
    }

    public void SetWeaponData(WeaponNames name)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i].Name == name)
            {
                currentData = datas[i];
                break;
            }
        }

        WeaponInfo info = new();

        info.Name = name;
        info.Level = currentData.Level;
        info.Tier = currentData.Tier;
        info.NameText = currentData.NameText;
        info.DescText = currentData.DescText;
        info.Icon = currentData.Icon;

        Info = info;

        RefreahWeaponStat();

        EventManager<EventTypes>.Send(EventTypes.EquipedWeapon, Name);
    }

    public void RefreahWeaponStat()
    {
        float addStat = 1 + InGameManager.Instance.Player.StatSystem.GetStat(StatNames.AddedWaeponStat);

        Info.Damage = currentData.Damage * (Info.Level * currentData.LevelByBonus) * addStat;
        Info.Speed = currentData.Speed;
        Info.CriticalRate = currentData.CriticalRate * addStat;
        Info.CriticalDamage = currentData.CriticalDamage * addStat;
    }

    public WeaponNames NextTier(int tier)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i].Tier == tier + 1)
            {
                return datas[i].Name;
            }
        }

        return WeaponNames.None;
    }
}