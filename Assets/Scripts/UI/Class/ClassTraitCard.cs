using TMPro;
using UnityEngine;

public class ClassTraitCard : MonoBehaviour
{
    public ClassTraitNames Name;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescriptionText;

    public XButton Button;

    private ClassTraitData data;

    private void Start()
    {
        Button.OnExecute = Click;
    }

    public void Refresh(ClassTraitNames name)
    {
        data = ResourcesManager.Instance.LoadScriptable<ClassTraitData>(name.ToString());

        if (data != null)
        {
            Name = name;

            NameText.SetText(data.NameString);
            DescriptionText.SetText("직업 : " + data.ClassName.GetTraitLanguage());
        }
    }

    public void Click()
    {
        InGameManager.Instance.Player.ClassTraitSystem.Add(Name);

        EventManager<EventTypes>.Send(EventTypes.AddTrait, Name);

        UIPopupManager.Instance.Despawn();
    }
}