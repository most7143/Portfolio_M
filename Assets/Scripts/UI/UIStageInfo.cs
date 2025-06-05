using TMPro;
using UnityEngine;

public class UIStageInfo : MonoBehaviour
{
    public TextMeshProUGUI NameText;

    public GameSpeedButton SpeedButton;

    public void Refresh(StageData stageData)
    {
        NameText.SetText(stageData.NameString);
    }
}