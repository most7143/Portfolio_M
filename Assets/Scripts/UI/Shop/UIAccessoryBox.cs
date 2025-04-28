using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAccessoryBox : MonoBehaviour
{
    public int Index;
    public AccessoryTypes Type;
    public Image Icon;
    public Image BackgroundImage;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI GradeText;
    public TextMeshProUGUI DescriptionText;

    public TextMeshProUGUI ValueText;

    public XButton RerollButton;

    private AccessoryItem item;

    private void Start()
    {
        RerollButton.OnExecute = ClickButton;
    }

    public void Refresh()
    {
        Player player = InGameManager.Instance.Player;

        item = InGameManager.Instance.Player.AccessorySystem.GetItem(Type);

        Icon.sprite = ResourcesManager.Instance.LoadSprite(string.Format("Icon_{0}_{1}", Type, item.Grade));

        BackgroundImage.sprite = ResourcesManager.Instance.LoadSprite(string.Format("Background_Pannel_{0}", item.Grade));

        if (item.Grade != GradeNames.None)
        {
            NameText.SetText(EXText.SetGradeColor(item.Grade, item.NameString));
            GradeText.SetText(EXText.SetGradeColor(item.Grade, item.Grade.GetGradeLanguage()));
            DescriptionText.SetText(item.DescriptionString);
        }
        else
        {
            NameText.SetText(Index + "번 슬롯");
            GradeText.SetText(" ");
        }

        ValueText.SetText(SetStats());
    }

    private string SetStats()
    {
        string value = string.Empty;
        if (item.Stats.Count > 0)
        {
            for (int i = 0; i < item.Stats.Count; i++)
            {
                StatNames name = item.Stats[i];
                float statValue = item.Values[i];

                value += name.GetStatLanguage() + " : " + GetPercent(name, statValue) + "\n";
            }
        }

        return value;
    }

    private string GetPercent(StatNames stat, float value)
    {
        switch (stat)
        {
            case StatNames.Attack:
            case StatNames.Armor:
            case StatNames.Health:
            case StatNames.AttackSpeed:
                return value.ToString();

            default:
                return string.Format("{0}%", value * 100f);
        }
    }

    public void ClickButton()
    {
        if (InGameManager.Instance.Controller.TryUsingCurrency(CurrencyTypes.Gem, 1))
        {
            InGameManager.Instance.Player.AccessorySystem.ResetOption(Type);
            InGameManager.Instance.Controller.UseCurrency(CurrencyTypes.Gem, 1);
        }
    }
}