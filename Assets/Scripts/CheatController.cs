using UnityEngine;

public class CheatController : MonoBehaviour
{
    public ClassNames ChangeClassName;

    public XButton StageFail;
    public XButton DataReset;
    public XButton Addgold;
    public XButton ChangeClass;

    private void Start()
    {
        StageFail.OnExecute = Fail;
        DataReset.OnExecute = ResetData;
        ChangeClass.OnExecute = Change;
        Addgold.OnExecute = AddGold;
    }

    public void Fail()
    {
        InGameManager.Instance.StageFail();
    }

    public void ResetData()
    {
        OutGameManager.Instance.TotalMonsterKillCount = 0;
        PlayerPrefs.DeleteAll();
    }

    public void Change()
    {
        InGameManager.Instance.Player.ClassTraitSystem.ChangeClass(ChangeClassName);
    }

    public void AddGold()
    {
        InGameManager.Instance.Controller.AddCurrency(CurrencyTypes.Gold, 100000);
        InGameManager.Instance.Controller.AddCurrency(CurrencyTypes.Gem, 100000);
    }
}