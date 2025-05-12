using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterInfo : MonoBehaviour
{
    public TextMeshProUGUI RankText;
    public TextMeshProUGUI ClassText;
    public TextMeshProUGUI DescText;

    public TextMeshProUGUI Class1ValueText;
    public Image Line1;

    public TextMeshProUGUI Class2ValueText;
    public Image Line2;
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

        RefreshClass(player.ClassTraitSystem.Name);
    }

    public void RefreshRank(int level)
    {
        RankText.SetText("Rank " + level);
    }

    public void RefreshClass(ClassNames name)
    {
        ClassText.SetText(name.GetClassLanguage());

        ClassData classData = ResourcesManager.Instance.LoadScriptable<ClassData>(name.ToString());

        RefresClassText(classData);

        Player player = InGameManager.Instance.Player;

        if (player.ClassTraitSystem.ClassTier == 1)
        {
            Line1.gameObject.SetActive(true);
            RefreshClassValueText(classData, Class1ValueText);
        }
        else if (player.ClassTraitSystem.ClassTier == 2)
        {
            Line2.gameObject.SetActive(true);
            RefreshClassValueText(classData, Class2ValueText);
            classData = ResourcesManager.Instance.LoadScriptable<ClassData>(player.ClassTraitSystem.PrevClass.ToString());
            RefreshClassValueText(classData, Class1ValueText);
        }
    }

    private void RefresClassText(ClassData classData)
    {
        Player player = InGameManager.Instance.Player;

        if (classData != null)
        {
            ClassText.SetText(classData.NameText);

            DescText.SetText(classData.DescritpionText);
        }
    }

    private void RefreshClassValueText(ClassData classData, TextMeshProUGUI text)
    {
        Player player = InGameManager.Instance.Player;

        string value = "[" + classData.NameText + "]" + "\n";

        if (classData.Stats.Count == 1)
        {
            value += string.Format(classData.StatDescriptionText, EXText.GetStatPercent(classData.Stats[0], classData.Values[0])) + "\n";
        }
        else if (classData.Stats.Count == 2)
        {
            value += string.Format(classData.StatDescriptionText,
                EXText.GetStatPercent(classData.Stats[0], classData.Values[0]), EXText.GetStatPercent(classData.Stats[1], classData.Values[1])) + "\n";
        }

        if (classData.Projectiles.Count == 1)
        {
            value += string.Format(classData.ProjectileDescritonText, player.ProjectileSystem.GetProjectile(classData.Projectiles[0]).Chance * 100f, player.ProjectileSystem.GetProjectile(classData.Projectiles[0]).DamgeRate * 100f) + "\n";
        }
        else if (classData.Projectiles.Count == 2)
        {
            value += string.Format(classData.ProjectileDescritonText, player.ProjectileSystem.GetProjectile(classData.Projectiles[0]).Chance * 100f, player.ProjectileSystem.GetProjectile(classData.Projectiles[0]).DamgeRate * 100f,
                player.ProjectileSystem.GetProjectile(classData.Projectiles[1]).Chance * 100f, player.ProjectileSystem.GetProjectile(classData.Projectiles[1]).DamgeRate * 100f) + "\n";
        }

        if (classData.BuffNames.Count == 1)
        {
            value += string.Format(classData.BuffDescritonText, classData.BuffValues[0] * 100f);
        }
        else if (classData.BuffNames.Count == 2)
        {
            value += string.Format(classData.BuffDescritonText, classData.BuffValues[0] * 100f, classData.BuffValues[1]);
        }

        text.SetText(value);
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