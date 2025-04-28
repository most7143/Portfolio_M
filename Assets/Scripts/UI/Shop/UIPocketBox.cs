using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPocketBox : MonoBehaviour
{
    public PocketTypes Type;
    public Image Icon;

    private PocketData data;

    public XButton Button;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescText;
    public TextMeshProUGUI CostText;

    private void Start()
    {
        Button.OnExecute = Click;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public void Init(PocketTypes type)
    {
        Type = type;

        Icon.sprite = ResourcesManager.Instance.LoadSprite(string.Format("Icon_{0}Pocket", Type.ToString()));

        data = ResourcesManager.Instance.LoadScriptable<PocketData>(Type.ToString() + "Pocket");

        if (data != null)
        {
            NameText.SetText(data.NameString);
            DescText.SetText(data.DescriptionString);
        }
    }

    public void Actiavte()
    {
        if (data == null)
            return;

        CostText.SetText(string.Format("{0}{1}", data.Cost.ToString(), "<sprite=1>"));

        if (InGameManager.Instance.Controller.TryUsingCurrency(CurrencyTypes.Gem, data.Cost))
        {
            CostText.color = Color.white;
        }
        else
        {
            CostText.color = Color.red;
        }
    }

    public void Click()
    {
        if (InGameManager.Instance.Controller.TryUsingCurrency(CurrencyTypes.Gem, data.Cost))
        {
            float rand = Random.Range(0, 1f);

            int selectIndex = data.Chance.Count - 1;

            float chance = 0;

            for (int i = 0; i < data.Chance.Count; i++)
            {
                chance += data.Chance[i];

                if (rand < chance)
                {
                    selectIndex = i;
                    break;
                }
            }

            InGameManager.Instance.Controller.UseCurrency(CurrencyTypes.Gem, data.Cost);

            int value = 0;

            if (Type == PocketTypes.Yellow)
            {
                value = Mathf.FloorToInt(InGameManager.Instance.MonsterSpanwer.Gold * data.Values[selectIndex]);
                InGameManager.Instance.Controller.AddCurrency(CurrencyTypes.Gold, value);
            }
            else if (Type == PocketTypes.Green || Type == PocketTypes.Red || Type == PocketTypes.Black)
            {
                int curGradeIndex = (int)InGameManager.Instance.Player.AccessorySystem.GetGrade(data.AccessoryType);
                int nexGradeIndx = (int)data.Grades[selectIndex];

                Debug.Log(curGradeIndex + " < " + nexGradeIndx);

                if (curGradeIndex > nexGradeIndx)
                {
                    InGameManager.Instance.Player.AccessorySystem.Upgrade(data.AccessoryType, data.Grades[selectIndex]);
                }
            }
        }
    }
}