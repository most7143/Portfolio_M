using TMPro;
using UnityEngine.UI;

public class UIWeaponDetails : UIDetails
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescText;
    public Image WeaponIcon;

    public void Refresh(WeaponNames weaponName)
    {
        WeaponData weaponData = ResourcesManager.Instance.LoadWeaponScriptable(weaponName);

        if (weaponData != null)
        {
            NameText.SetText(weaponData.NameText);
            DescText.SetText(weaponData.DescText);
            WeaponIcon.sprite = weaponData.Icon;
        }
    }
}