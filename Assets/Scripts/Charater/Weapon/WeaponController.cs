using System;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponInfo
{
    public WeaponNames Name;
    public string NameString;
    public int Tier;
    public int Level;
    public int LevelByBonus;
    public float Speed;
    public float Damage;
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
            WeaponData weaponData = Resources.Load<WeaponData>("ScriptableObject/Weapon/" + weaponNames[i].ToString());
            if (weaponData != null)
            {
                datas.Add(weaponData);
            }
        }

        SetWeaponData(WeaponNames.WoodenSword);
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
            Info.NameString = currentData.NameString;
            Info.Level = currentData.Level;
            Info.LevelByBonus = currentData.LevelByBonus;
            Info.Speed = currentData.Speed;
            Info.Damage = currentData.Damage;
            Info.Icon = currentData.Icon;
        }
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