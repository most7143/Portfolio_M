using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassTraitCard : MonoBehaviour
{
    public ClassTraitNames Name;
    public GradeNames Grade;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI StatText;
    public TextMeshProUGUI CountText;

    public Image BackGround;
    public Image IconImage;

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

        int tier = 0;

        if (InGameManager.Instance.Player.ClassTraitSystem.Name == ClassNames.Swordman)
        {
            tier = 0;
        }
        else
        {
            tier = 1;
        }

        Grade = EXValue.GetChanceByGrade(2f);

        if (data != null)
        {
            Name = name;
            count = GetGradeByCount();
            NameText.SetText(data.NameText);
            IconImage.sprite = ResourcesManager.Instance.LoadSprite("Icon_Trait_" + Name.ToString());
            BackGround.sprite = ResourcesManager.Instance.LoadSprite("BackgroundCard_" + Grade.ToString());
            CountText.SetText("특성 +" + count);
            StatText.SetText(EXText.GetGradeColor(Grade, Grade.GetGradeLanguage()));

            ClassNames className = data.ClassNames[tier];

            ClassData classData = ResourcesManager.Instance.LoadScriptable<ClassData>(className.ToString());

            if (tier < 1)
            {
                EXText.RefreshClassValueText(classData, StatText);
            }
            else
            {
                StatText.SetText(string.Format("[{0}] <br>???", EXText.GetClassLanguage(className)));
            }
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