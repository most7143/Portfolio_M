public class UICharacterTab : UITab
{
    public UICharacterInfo CharacterInfo;

    public override void Activate()
    {
        base.Activate();

        CharacterInfo.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}