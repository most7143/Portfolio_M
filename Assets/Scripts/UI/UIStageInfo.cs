using TMPro;
using UnityEngine;

public class UIStageInfo : MonoBehaviour
{
    public TextMeshProUGUI NameText;

    public GameSpeedButton SpeedButton;

    private void Start()
    {
    }

    public void Refresh(StageData stageData)
    {
        NameText.SetText(stageData.NameString);
    }
}