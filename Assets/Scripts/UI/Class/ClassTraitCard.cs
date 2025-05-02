using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassTraitCard : MonoBehaviour
{
    public ClassTraitNames Name;
    public GradeNames Grade;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI GradeText;
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI CountText;

    public Image BackGround;

    public XButton Button;

    private int count = 1;

    private ClassTraitData data;

    private void Start()
    {
        Button.OnExecute = Click;
    }

    public void Refresh(ClassTraitNames name)
    {
        data = ResourcesManager.Instance.LoadScriptable<ClassTraitData>(name.ToString());

        int tier = InGameManager.Instance.Player.ClassTraitSystem.ClassTier;

        Grade = EXValue.GetChanceByGrade();

        if (data != null)
        {
            Name = name;
            count = GetGradeByCount();
            NameText.SetText(data.NameString);
            BackGround.sprite = ResourcesManager.Instance.LoadSprite("BackgroundCard_" + Grade.ToString());
            DescriptionText.SetText("직업 : " + data.ClassNames[tier].GetClassLanguage());
            CountText.SetText(data.NameString + " +" + count);
            GradeText.SetText(EXText.GetGradeColor(Grade, Grade.GetGradeLanguage()));
        }
    }

    public void Click()
    {
        InGameManager.Instance.Player.ClassTraitSystem.Add(Name, count);

        EventManager<EventTypes>.Send(EventTypes.AddTrait, Name);

        UIPopupManager.Instance.Despawn();
    }

    public int GetGradeByCount()
    {
        switch (Grade)
        {
            case GradeNames.Mythic:
                return 30;

            case GradeNames.Legendary:
                return 15;

            case GradeNames.Unique:
                return 10;

            case GradeNames.Magic:
                return 7;

            case GradeNames.Rare:
                return 3;

            case GradeNames.Normal:
                return 1;
        }

        return 1;
    }
}