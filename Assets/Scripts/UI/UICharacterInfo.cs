using DG.Tweening;
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

    private Vector3 originGroup;

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
        originGroup = StatGroup.Rect.anchoredPosition3D;
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
        StatGroup.Refresh();

        float duration = 0.5f;
        float targetX = _isStatInfo ? originGroup.x : originGroup.x - 150;
        float alpha = _isStatInfo ? 0 : 1;

        StatGroup.Rect.DOAnchorPosX(targetX, duration).SetEase(Ease.OutQuad);
        StatGroup.CanvasGroup.DOFade(alpha, duration);
        _isStatInfo = !_isStatInfo;
    }
}