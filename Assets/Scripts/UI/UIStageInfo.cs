using TMPro;
using UnityEngine;

public class UIStageInfo : MonoBehaviour
{
    public TextMeshProUGUI NameText;

    public GameSpeedButton SpeedButton;

    public XButton CheatButton;

    public XButton TextButton;

    public XButton TestButton2;

    public ClassNames ClassName;
    public ClassNames ClassName2;
    private int test;

    private void Start()
    {
        CheatButton.OnExecute = Cheat;
        TextButton.OnExecute = Test;
        TestButton2.OnExecute = Test2;
    }

    public void Refresh(StageData stageData)
    {
        NameText.SetText(stageData.NameString);
    }

    public void Cheat()
    {
        InGameManager.Instance.Controller.AddCurrency(CurrencyTypes.Gold, 100000);
        InGameManager.Instance.Controller.AddCurrency(CurrencyTypes.Gem, 100000);
    }

    public void Test()
    {
        test++;

        if (test == 1)
        {
            InGameManager.Instance.Player.ClassTraitSystem.ChangeClass(ClassName);
        }
        else
        {
            InGameManager.Instance.Player.ClassTraitSystem.ChangeClass(ClassName2);
        }
    }

    public void Test2()
    {
        InGameManager.Instance.Player.StatSystem.AddStat(StatTID.Elite, StatNames.Invincibility, 1);
    }
}