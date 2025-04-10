public class UIWeaponTab : UITab
{
    public UIWeaponInfo WeaponInfo;

    public override void Activate()
    {
        base.Activate();

        WeaponInfo.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}