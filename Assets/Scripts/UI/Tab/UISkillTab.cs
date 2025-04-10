public class UISkillTab : UITab
{
    public UIPassiveSkillInfo UISkillInfo;

    public override void Activate()
    {
        base.Activate();
        UISkillInfo.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}