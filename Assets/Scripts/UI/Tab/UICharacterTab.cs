public class UICharacterTab : UITab
{
    public UICharacterInfo CharacterInfo;

    public override void Activate()
    {
        base.Activate();

        CharacterInfo.Group.alpha = 1;
        CharacterInfo.Group.blocksRaycasts = true;
        CharacterInfo.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
        CharacterInfo.Group.alpha = 0;
        CharacterInfo.Group.blocksRaycasts = false;
    }
}