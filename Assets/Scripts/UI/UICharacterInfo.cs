using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterInfo : MonoBehaviour
{
    public TextMeshProUGUI RankText;
    public TextMeshProUGUI ClassText;
    public TextMeshProUGUI DescText;
    public TextMeshProUGUI StatText;

    public Sprite ClassIcon;

    public Button StatButton;
    public UIBaseStatGroup StatGroup;
    private bool _isStatInfo;

    private Vector3 originGroup;

    private void OnEnable()
    {
        EventManager<EventTypes>.Register<int>(EventTypes.LevelUp, RefreshRank);
        EventManager<EventTypes>.Register<ClassNames>(EventTypes.ChangeClass, RefreshClass);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister<int>(EventTypes.LevelUp, RefreshRank);
        EventManager<EventTypes>.Unregister<ClassNames>(EventTypes.ChangeClass, RefreshClass);
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
        RefresClassText(player.ClassTraitSystem.Class);
    }

    public void RefreshRank(int level)
    {
        RankText.SetText("Rank " + level);
    }

    public void RefreshClass(ClassNames name)
    {
        ClassText.SetText(name.GetClassLanguage());
        RefresClassText(name);
    }

    private void RefresClassText(ClassNames className)
    {
        ClassData classData = ResourcesManager.Instance.LoadScriptable<ClassData>(className.ToString());

        if (classData != null)
        {
            ClassText.SetText(classData.NameText);
            DescText.SetText(classData.DescritpionText);

            if (classData.Stats.Count == 1)
            {
                StatText.SetText(classData.NameText + " 효과 : " + string.Format(classData.StatDescriptionText, EXText.GetStatPercent(classData.Stats[0], classData.Values[0])));
            }
            else if (classData.Stats.Count == 2)
            {
                StatText.SetText(classData.NameText + " 효과 : " + string.Format(classData.StatDescriptionText,
                    EXText.GetStatPercent(classData.Stats[0], classData.Values[0]), EXText.GetStatPercent(classData.Stats[1], classData.Values[1])));
            }
        }
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