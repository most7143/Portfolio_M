using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPocketBox : MonoBehaviour
{
    public PocketTypes Type;
    public Image Icon;

    private PocketData data;

    public XButton Button;
    public RectTransform FloatyPoint;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescText;
    public TextMeshProUGUI CostText;

    public Image LockImage;
    public TextMeshProUGUI LockText;

    private void Start()
    {
        LockImage.gameObject.SetActive(true);
        Button.OnExecute = Click;
    }

    private void OnEnable()
    {
        EventManager<EventTypes>.Register<int>(EventTypes.LevelUp, UnLock);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister<int>(EventTypes.LevelUp, UnLock);
    }

    public void UnLock(int level)
    {
        if (data != null)
        {
            if (data.UnlockRank <= level)
            {
                LockImage.gameObject.SetActive(false);
            }
        }
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
            LockText.SetText(data.UnlockRank + " 랭크 달성 시 해금");
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

        UnLock(InGameManager.Instance.Player.Level);
    }

    public void Click()
    {
        if (InGameManager.Instance.Controller.TryUsingCurrency(CurrencyTypes.Gem, data.Cost))
        {
            float rand = Random.Range(0, 1f);

            int selectIndex = (int)EXValue.GetChanceByGrade() - 1;

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

                if (curGradeIndex == 1)
                {
                    InGameManager.Instance.ObjectPool.SpawnFloaty(FloatyPoint.position, FloatyTypes.Fail, "구매 불가");
                    return;
                }

                if (curGradeIndex == 0 || curGradeIndex > nexGradeIndx)
                {
                    InGameManager.Instance.Player.AccessorySystem.Upgrade(data.AccessoryType, data.Grades[selectIndex]);
                    InGameManager.Instance.ObjectPool.SpawnFloaty(FloatyPoint.position, FloatyTypes.Success, "성공!");
                }
                else
                {
                    InGameManager.Instance.ObjectPool.SpawnFloaty(FloatyPoint.position, FloatyTypes.Fail, "실패");
                }
            }

            InGameManager.Instance.Controller.UseCurrency(CurrencyTypes.Gem, data.Cost);
        }
    }
}