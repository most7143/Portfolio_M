using TMPro;
using UnityEngine;

public class UIStageInfo : MonoBehaviour
{
    public TextMeshProUGUI NameText;

    public GameSpeedButton SpeedButton;

    public XButton CheatButton;

    private void Start()
    {
        CheatButton.OnExecute = Cheat;
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
}