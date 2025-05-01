using TMPro;
using UnityEngine.UI;

public class UIWeaponDetails : UIDetails
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescText;
    public Image WeaponIcon;

    public void Refresh(WeaponNames weaponName)
    {
        WeaponData weaponData = ResourcesManager.Instance.LoadScriptable<WeaponData>(weaponName.ToString());

        if (weaponData != null)
        {
            NameText.SetText(weaponData.NameString);
            DescText.SetText(weaponData.DescString);
            WeaponIcon.sprite = weaponData.Icon;
        }
    }
}