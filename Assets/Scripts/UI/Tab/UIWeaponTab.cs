public class UIWeaponTab : UITab
{
    public WeaponUpgrade Upgrade;

    public override void Activate()
    {
        base.Activate();

        Upgrade.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}