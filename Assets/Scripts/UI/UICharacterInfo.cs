using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterInfo : MonoBehaviour
{
    public TextMeshProUGUI RankText;
    public TextMeshProUGUI ClassText;
    public Sprite ClassIcon;

    public Button StatButton;
    public UIBaseStatGroup StatGroup;
    private bool _isStatInfo;

    private void OnEnable()
    {
        EventManager<EventTypes>.Register<int>(EventTypes.LevelUp, RefreshRank);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister<int>(EventTypes.LevelUp, RefreshRank);
    }

    private void Start()
    {
        StatButton.onClick.AddListener(() => StatButtonClick());
    }

    public void Activate()
    {
        Player player = InGameManager.Instance.Player;
        RefreshRank(player.Level);
    }

    public void RefreshRank(int level)
    {
        RankText.SetText("Rank " + level);
    }

    public void StatButtonClick()
    {
        if (false == _isStatInfo)
        {
            ShowStat();
        }
        else
        {
            HideStat();
        }
    }

    private void ShowStat()
    {
        StatGroup.Refresh();
        StatGroup.CanvasGroup.alpha = 1;
        _isStatInfo = true;
    }

    private void HideStat()
    {
        StatGroup.CanvasGroup.alpha = 0;
        _isStatInfo = false;
    }
}