using TMPro;
using UnityEngine;

public class UIChallengeBox : MonoBehaviour
{
    public UIChallengeNames Name;
    public StatNames StatName;
    public string StatNameString;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescText;
    public TextMeshProUGUI RequiredText;
    public TextMeshProUGUI NextRewardText;

    private ChallengeData _data;

    private int rank = -1;

    public void Refresh(ChallengeData data)
    {
        _data = data;
        Name = data.Name;
        NameText.SetText(_data.NameText);
        DescText.SetText(_data.DescText);

        SetRank();

        string requiredString = "달성 조건 : ";
        if (OutGameManager.Instance.GetChallengeValue(Name) >= _data.RequireValues[0])
        {
            requiredString += _data.RequiredText;
        }
        else
        {
            requiredString += "???";
        }

        if (rank < 2)
        {
            requiredString += string.Format(" ({0} / {1})",
     OutGameManager.Instance.GetChallengeValue(Name), _data.RequireValues[rank + 1]);
        }

        RequiredText.SetText(requiredString);

        Vector2 rectSize = DescText.rectTransform.sizeDelta;
        rectSize.y = DescText.preferredHeight;
        DescText.rectTransform.sizeDelta = rectSize;
    }

    private void SetRank()
    {
        for (int i = 0; i <= 2; i++)
        {
            if (OutGameManager.Instance.GetChallengeValue(Name) >= _data.RequireValues[i])
            {
                rank = i;
            }
        }
    }
}