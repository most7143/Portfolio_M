using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAccessoryBox : MonoBehaviour
{
    public int Index;
    public ImageLoop BackgroundAnim;
    public AccessoryTypes Type;
    public Image Icon;
    public Image BackgroundImage;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI GradeText;
    public TextMeshProUGUI DescriptionText;

    public TextMeshProUGUI ValueText;

    public XButton RerollButton;

    public Image LockImage;
    public TextMeshProUGUI LockText;

    private AccessoryItem item;

    private void Start()
    {
        LockImage.gameObject.SetActive(true);
        LockText.SetText(string.Format("장신구 <style=Legendary>{0}</style> 획득 시 구매 가능", Index));
        RerollButton.IsPressClick = false;
        RerollButton.RefreshInteraction(false);
        RerollButton.OnExecute = ClickButton;

        BackgroundAnim.gameObject.SetActive(false);
    }

    public void Refresh()
    {
        Player player = InGameManager.Instance.Player;

        item = InGameManager.Instance.Player.AccessorySystem.GetItem(Type);

        Icon.sprite = ResourcesManager.Instance.LoadSprite(string.Format("Icon_{0}_{1}", Type, item.Grade));

        BackgroundImage.sprite = ResourcesManager.Instance.LoadSprite(string.Format("Background_Pannel_{0}", item.Grade));

        if (item.Grade != GradeNames.None)
        {
            NameText.SetText(EXText.GetGradeColor(item.Grade, item.NameString));
            GradeText.SetText(EXText.GetGradeColor(item.Grade, item.Grade.GetGradeLanguage()));
            DescriptionText.SetText(EXText.GetGradeColor(item.Grade, item.DescriptionString));
        }
        else
        {
            NameText.SetText(Index + "번 슬롯");
            GradeText.SetText(" ");
        }

        ValueText.SetText(SetStats());

        if (item.Grade != GradeNames.None)
        {
            RerollButton.RefreshInteraction(true);
            LockImage.gameObject.SetActive(false);
        }

        if (item.Grade == GradeNames.Mythic)
        {
            BackgroundAnim.gameObject.SetActive(true);
        }
    }

    private string SetStats()
    {
        string value = string.Empty;

        if (item.Values.Count > 0)
        {
            for (int i = 0; i < item.Values.Count; i++)
            {
                StatNames name = item.Values[i].Name;
                float statValue = item.Values[i].Value;

                value += name.GetStatLanguage() + " : " + GetColor(item.Values[i].Rank, EXText.GetStatPercent(name, statValue)) + "\n";
            }
        }

        return value;
    }

    private string GetColor(int rank, string value)
    {
        if (rank == 1)
        {
            return string.Format("<style=Mythic>{0}</style>", value);
        }
        else if (rank == 2)
        {
            return string.Format("<color=white>{0}</color>", value);
        }
        else
        {
            return string.Format("<style=Normal>{0}</style>", value);
        }
    }

    public void ClickButton()
    {
        if (InGameManager.Instance.Controller.TryUsingCurrency(CurrencyTypes.Gem, 1))
        {
            float rand = Random.Range(0, 1f);

            if (rand < InGameManager.Instance.Player.StatSystem.GetStat(StatNames.AccOptionRerollFreeGem))
            {
                InGameManager.Instance.Controller.UseCurrency(CurrencyTypes.Gem, 1);
            }

            InGameManager.Instance.Player.AccessorySystem.ResetOption(Type);
        }
    }
}