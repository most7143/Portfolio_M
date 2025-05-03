using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITraitSlot : MonoBehaviour
{
    public ClassTraitNames Name;
    public ClassNames ClassName;
    public bool IsSet = false;
    public Image Background;
    public Image IconImage;
    public TextMeshProUGUI CountText;

    public XButton Button;
    public ImageLoop LoopImage;

    private bool _isChange;

    private ClassTraitData data;

    private void Start()
    {
        Button.OnExecute = ClassChange;
    }

    public void Init(ClassTraitNames name)
    {
        data = ResourcesManager.Instance.LoadScriptable<ClassTraitData>(name.ToString());
        Name = name;
        IsSet = true;

        Refresh(0);
    }

    public void Refresh(int tier)
    {
        ClassName = data.ClassNames[tier];
        IconImage.sprite = ResourcesManager.Instance.LoadSprite("Icon_Trait_" + Name.ToString());
        int count = InGameManager.Instance.Player.ClassTraitSystem.Traits[Name];

        if (count == 0)
        {
            CountText.SetText("");
        }
        else
        {
            CountText.SetText(count + " / " + InGameManager.Instance.Player.ClassTraitSystem.MaxLevel);
        }

        RefreshClassChange(count);
    }

    public void RefreshClassChange(int count)
    {
        if (count >= InGameManager.Instance.Player.ClassTraitSystem.MaxLevel)
        {
            LoopImage.transform.position = Background.transform.position;
            LoopImage.enabled = true;
            LoopImage.Group.alpha = 1;
            _isChange = true;
        }
        else
        {
            LoopImage.enabled = false;
            LoopImage.Group.alpha = 0;
            _isChange = false;
        }
    }

    public void ClassChange()
    {
        if (_isChange)
        {
            InGameManager.Instance.Player.ClassTraitSystem.ChangeClass(ClassName);
            UIPopupManager.Instance.Despawn();
        }
    }
}