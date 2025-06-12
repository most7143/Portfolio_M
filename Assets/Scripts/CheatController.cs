using UnityEngine;

public class CheatController : MonoBehaviour
{
    public ClassNames ChangeClassName;

    public XButton StageFail;
    public XButton DataReset;
    public XButton Addgold;
    public XButton ChangeClass;
    public XButton AllChallenge;
    public int AllChallengeCount = 0;

    private void Start()
    {
        StageFail.OnExecute = Fail;
        DataReset.OnExecute = ResetData;
        ChangeClass.OnExecute = Change;
        Addgold.OnExecute = AddGold;
        AllChallenge.OnExecute = AllChallenges;
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
        InGameManager.Instance.Controller.AddCurrency(CurrencyTypes.Gem, 15);
    }

    public void AllChallenges()
    {
        ChallengeData[] datas = OutGameManager.Instance.GetChallengeDatas();

        if (datas.Length > 0)
        {
            for (int i = 0; i < datas.Length; i++)
            {
                OutGameManager.Instance.SetChallengeDataValue(datas[i].Name, (int)datas[i].RequireValues[AllChallengeCount]);
            }
        }
    }
}