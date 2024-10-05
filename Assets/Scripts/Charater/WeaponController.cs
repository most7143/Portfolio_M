using UnityEngine;

public struct WeaponInfo
{
    public WeaponNames Name;
    public string NameString;
    public int Level;
    public float Speed;
    public float Damage;
}

public class WeaponController : MonoBehaviour
{
    public WeaponNames Name = WeaponNames.WoodenSword;
    public WeaponInfo Info;

    private WeaponData data;

    public void SetBaseData()
    {
        data = Resources.Load<WeaponData>("ScriptableObject/Weapon/" + Name.ToString());

        if (data != null)
        {
            Info.Name = data.Name;
            Info.NameString = data.NameString;
            Info.Level = data.Level;
            Info.Speed = data.Speed;
            Info.Damage = data.Damage;
        }
    }
}