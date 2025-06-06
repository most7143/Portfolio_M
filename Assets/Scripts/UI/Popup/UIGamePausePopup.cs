using UnityEngine.SceneManagement;

public class UIGamePausePopup : UIPopup
{
    public XButton ContinueButton;
    public XButton TitleButton;

    private void Start()
    {
        TitleButton.OnExecute = Title;
        TitleButton.IsPressClick = false;

        ContinueButton.OnExecute = Continue;
        ContinueButton.IsPressClick = false;
    }

    public override void Spawn()
    {
        InGameManager.Instance.PauseBattle();

        base.Spawn();
    }

    public override void Despawn()
    {
        if (UIPopupManager.Instance.Name == UIPopupNames.None)
        {
            InGameManager.Instance.ContinueBattle();
        }
    }

    public void Continue()
    {
        UIPopupManager.Instance.DespawnPausePopup();
    }

    public void Title()
    {
        InGameManager.Instance.SaveData();
        SceneManager.LoadScene("TitleScene");
        UIPopupManager.Instance.DespawnPausePopup();
    }
}