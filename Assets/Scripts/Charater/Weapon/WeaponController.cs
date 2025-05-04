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
    public int LevelByBonus;
    public float Speed;
    public float Damage;
    public float CriticalRate;
    public float CriticalDamage;
    public Sprite Icon;
}

public class WeaponController : MonoBehaviour
{
    public WeaponNames Name = WeaponNames.WoodenSword;
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

        if (currentData != null)
        {
            Info.Tier = currentData.Tier;
            Info.Name = currentData.Name;
            Info.SkillNames = currentData.SkillNames.ToArray();
            Info.NameText = currentData.NameText;
            Info.DescText = currentData.DescText;
            Info.Level = currentData.Level;
            Info.LevelByBonus = currentData.LevelByBonus;
            Info.Speed = currentData.Speed;
            Info.Damage = currentData.Damage;
            Info.CriticalRate = currentData.CriticalRate;
            Info.CriticalDamage = currentData.CriticalDamage;
            Info.Icon = currentData.Icon;
        }

        Name = Info.Name;

        EventManager<EventTypes>.Send(EventTypes.EquipedWeapon, Name);
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