public class UISkillTab : UITab
{
    public UISkillInfo UISkillInfo;

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