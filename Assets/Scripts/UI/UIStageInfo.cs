using TMPro;
using UnityEngine;

public class UIStageInfo : MonoBehaviour
{
    public TextMeshProUGUI NameText;

    public void Refresh(StageData stageData)
    {
        NameText.SetText(stageData.NameString);
    }
}