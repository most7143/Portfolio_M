using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPocketBox : MonoBehaviour
{
    public RectTransform Rect;
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

    private int _currentCost = 0;

    private int _useCount = 0;

    private void Start()
    {
        LockImage.gameObject.SetActive(true);
        Button.OnExecute = Click;
    }

    private void OnEnable()
    {
        EventManager<EventTypes>.Register(EventTypes.MonsterSpawnd, UnLock);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister(EventTypes.MonsterSpawnd, UnLock);
    }

    public void UnLock()
    {
        if (data != null)
        {
            if (InGameManager.Instance.MonsterSpanwer.Level >= data.UnlockKillMonster)
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
            LockText.SetText(string.Format("몬스터 <style=Legendary>{0}</style>마리 처치 시 구매 가능", data.UnlockKillMonster));
        }

        Rect.localScale = Vector3.one;
    }

    public void Actiavte()
    {
        if (data == null)
            return;

        if (Type == PocketTypes.Yellow)
        {
            _currentCost = data.Cost + (_useCount / 5);
        }
        else
        {
            _currentCost = data.Cost;
        }

        CostText.SetText(string.Format("{0}{1}", _currentCost, "<sprite=1>"));

        if (InGameManager.Instance.Controller.TryUsingCurrency(CurrencyTypes.Gem, _currentCost))
        {
            CostText.color = Color.white;
        }
        else
        {
            CostText.color = Color.red;
        }

        UnLock();
    }

    public void Click()
    {
        if (InGameManager.Instance.Controller.TryUsingCurrency(CurrencyTypes.Gem, _currentCost))
        {
            InGameManager.Instance.Controller.UseCurrency(CurrencyTypes.Gem, _currentCost);

            _useCount++;

            float rand = Random.Range(0, 1f);

            int selectIndex = (int)EXValue.GetChanceByGrade() - 1;

            int value = 0;

            if (Type == PocketTypes.Yellow)
            {
                value = Mathf.FloorToInt(InGameManager.Instance.MonsterSpanwer.Gold * data.Values[selectIndex]);
                InGameManager.Instance.Controller.AddCurrency(CurrencyTypes.Gold, value);
                InGameManager.Instance.ObjectPool.SpawnFloaty(FloatyPoint.position, FloatyTypes.Success, data.Values[selectIndex] + "배!");
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
                    SoundManager.Instance.Play(SoundNames.Success);
                }
                else
                {
                    InGameManager.Instance.ObjectPool.SpawnFloaty(FloatyPoint.position, FloatyTypes.Fail, "실패");
                }
            }

            OutGameManager.Instance.CheckWeaponOnlySpend = false;
        }
    }
}