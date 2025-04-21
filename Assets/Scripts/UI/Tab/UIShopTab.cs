public class UIShopTab : UITab
{
    private UIShopInfo ShopInfo;

    public override void Activate()
    {
        base.Activate();
        ShopInfo.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}