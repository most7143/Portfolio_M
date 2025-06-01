using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChallengeBox : MonoBehaviour
{
    public UIChallengeNames Name;
    public GradeNames Grade;
    public StatNames StatName;
    public string StatNameString;

    public Image Background;
    public ImageLoop ImageLoop;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescText;
    public TextMeshProUGUI RequiredText;
    public TextMeshProUGUI RewardText;

    private ChallengeData _data;

    private int rank = -1;

    public void Refresh(ChallengeData data)
    {
        _data = data;
        Name = data.Name;

        SetRank();
        SetGrade();
        SetBackGround();

        SetNameText();
        SetDescText();

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

        if (Grade == GradeNames.Mythic)
        {
            RequiredText.SetText(string.Format("<style={0}>{1}</style>", Grade.ToString(), "달성"));
        }
        else
        {
            RequiredText.SetText(requiredString);
        }

        Vector2 rectSize = DescText.rectTransform.sizeDelta;
        rectSize.y = DescText.preferredHeight;
        DescText.rectTransform.sizeDelta = rectSize;

        if (rank >= 0)
        {
            RewardText.SetText(string.Format("보상 : {0} {1}", EXText.GetStatLanguage(data.StatName), EXText.GetStatPercent(data.StatName, _data.StatValues[rank])));
        }
        else
        {
            RewardText.SetText("보상 : ???");
        }
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

    private void SetNameText()
    {
        string nameText = "";

        if (Grade == GradeNames.Mythic)
        {
            nameText = _data.MaxRankNameText;
        }
        else
        {
            nameText = _data.NameText;
        }

        NameText.SetText(string.Format("<style={0}>{1}</style>", Grade.ToString(), nameText));
    }

    private void SetDescText()
    {
        string descText = "";

        if (Grade == GradeNames.Mythic)
        {
            descText = _data.MaxRankDescText;
            DescText.color = Color.white;
        }
        else
        {
            descText = _data.DescText;
            DescText.color = Color.gray;
        }

        DescText.SetText(descText);
    }

    private void SetGrade()
    {
        switch (rank)
        {
            case -1:
                {
                    Grade = GradeNames.Normal;
                }
                break;

            case 0:
                {
                    Grade = GradeNames.Rare;
                }
                break;

            case 1:
                {
                    Grade = GradeNames.Legendary;
                }
                break;

            case 2:
                {
                    Grade = GradeNames.Mythic;
                }
                break;
        }
    }

    private void SetBackGround()
    {
        Background.sprite = ResourcesManager.Instance.LoadSprite("Background_Pannel_" + Grade.ToString());

        if (Grade == GradeNames.Mythic)
        {
            ImageLoop.gameObject.SetActive(true);
        }
    }
}