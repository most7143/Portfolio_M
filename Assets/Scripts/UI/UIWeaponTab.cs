public class UIWeaponTab : UITab
{
    public WeaponUpgrade Upgrade;

    public override void Activate()
    {
        base.Activate();

        Upgrade.gameObject.SetActive(true);
        Upgrade.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();

        Upgrade.gameObject.SetActive(false);
    }
}