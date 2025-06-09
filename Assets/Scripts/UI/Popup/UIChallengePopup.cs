using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class UIChallengePopup : UIPopup
{
    public List<UIChallengeBox> Boxes;

    public TextMeshProUGUI TitleNameText;
    public TextMeshProUGUI TitleDescText;
    public TextMeshProUGUI HelpText;

    public List<UIChallengeNames> ChallengeNames = new();

    public override void Spawn()
    {
        base.Spawn();
        UIChallengeNames[] names = Enum.GetValues(typeof(UIChallengeNames)).Cast<UIChallengeNames>().ToArray();
        for (int i = 1; i < Boxes.Count + 1; i++)
        {
            if (false == ChallengeNames.Contains(names[i]))
            {
                ChallengeNames.Add(names[i]);
            }

            Boxes[i - 1].Refresh(OutGameManager.Instance.GetChallengeData(names[i]));
        }

        Refresh();
    }

    public override void Despawn()
    {
        base.Despawn();
    }

    private void Refresh()
    {
        UIChallengeNames titleName = OutGameManager.Instance.GetAllClearByTitle();

        if (UIChallengeNames.None != titleName)
        {
            if (OutGameManager.Instance.ChallengeTitleName == UIChallengeNames.None)
            {
                OutGameManager.Instance.ChallengeTitleName = titleName;
            }

            GradeNames grade = GradeNames.Rare;

            if (titleName == UIChallengeNames.RemnantsOfWillpower)
            {
                grade = GradeNames.Mythic;
            }
            else if (titleName == UIChallengeNames.FootprintsLeftBehind)
            {
                grade = GradeNames.Legendary;
            }

            ChallengeData data = ResourcesManager.Instance.LoadScriptable<ChallengeData>("Challenge_" + titleName.ToString());

            TitleNameText.SetText(string.Format("<style={0}>{1}</style>", grade, data.NameText));

            TitleDescText.SetText(string.Format("<style={0}>{1}<br>({2} : {3})</style>", grade, data.DescText,
                EXText.GetStatLanguage(data.StatName), EXText.GetStatPercent(data.StatName, data.StatValues[0])));

            HelpText.gameObject.SetActive(false);
        }
    }
}