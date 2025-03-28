﻿public class UIWeaponTab : UITab
{
    public WeaponUpgrade Upgrade;

    public override void Activate()
    {
        base.Activate();

        Upgrade.Group.alpha = 1;
        Upgrade.Group.blocksRaycasts = true;
        Upgrade.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
        Upgrade.Group.alpha = 0;
        Upgrade.Group.blocksRaycasts = false;
    }
}