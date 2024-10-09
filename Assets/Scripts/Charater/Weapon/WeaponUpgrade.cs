using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgrade : MonoBehaviour
{
    public Button UprageButton;

    public TextMeshProUGUI PercentText;

    private int currentSucPercent;
    private int baseSucPercent = 95;

    private void Start()
    {
        UprageButton.onClick.AddListener(Upgrade);
        currentSucPercent = baseSucPercent;
        RefreshPercentText();
    }

    public void Upgrade()
    {
        Player player = InGameManager.Instance.Player;
        WeaponInfo weaponInfo = player.WeaponController.Info;

        if (TryUpgrade(weaponInfo))
        {
            if (player != null)
            {
                player.WeaponController.Info.Damage += GetAddDamage(weaponInfo);
                player.WeaponController.Info.Level += 1;

                player.RefreshWeaponInfo();
                InGameManager.Instance.PlayerInfo.RefreshWeaponInfo(player.WeaponController.Info);

                SetPecent(player.WeaponController.Info.Level);
                RefreshPercentText();
            }
        }
    }

    private bool TryUpgrade(WeaponInfo weaponInfo)
    {
        int range = Random.Range(0, 100);

        return range < currentSucPercent;
    }

    public int GetAddDamage(WeaponInfo weaponInfo)
    {
        return 1 + (weaponInfo.Level / weaponInfo.LevelByBonus);
    }

    private void SetPecent(int level)
    {
        int percent = baseSucPercent - (5 * (level / 3));

        if (percent < 5)
        {
            percent = 5;
        }

        currentSucPercent = percent;
    }

    private void RefreshPercentText()
    {
        PercentText.SetText("¼º°ø È®·ü : " + currentSucPercent.ToString() + "% \n"
            + "½ÇÆÐ È®·ü : " + (100 - currentSucPercent).ToString() + "%");
    }
}