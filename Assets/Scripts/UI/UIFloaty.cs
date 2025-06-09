using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public enum FloatyTypes
{
    None,
    Damage,
    CritialDamage,
    SkillDamage,
    CritialSkillDamage,
    Success,
    Fail,
    Gold,
    Gem,
    Dodge,
    Heal,
}

public enum FloatyPoints
{
    None,
    Owner,
    Target,
}

public class UIFloaty : MonoBehaviour
{
    public Canvas Canvas;
    public UIFade Fade;
    public CanvasGroup CanvasGroup;
    public FloatyTypes Type;
    public RectTransform CanvasRect;
    public RectTransform Rect;
    public TextMeshProUGUI Text;
    public bool IsAlive;
    public float AliveTime;

    public Vector3 Offset;

    private int _widthSize;

    private float _fontSize;

    public void Init()
    {
        Text.gameObject.SetActive(false);
        _fontSize = Text.fontSize;
    }

    public void Spawn(Vector2 position, FloatyTypes type, string text)
    {
        Type = type;
        Text.text = text;
        IsAlive = true;
        CanvasGroup.alpha = 1;
        SetColor(type);
        Text.gameObject.SetActive(true);
        StartCoroutine(ProcessAlive());
        RefreshSize(type);

        if (type == FloatyTypes.Damage || type == FloatyTypes.CritialDamage || type == FloatyTypes.CritialSkillDamage || type == FloatyTypes.SkillDamage || type == FloatyTypes.Dodge || type == FloatyTypes.Heal)
        {
            Rect.anchoredPosition = GetWorldPosition(position);
        }
        else
        {
            Rect.position = position;
        }

        Fade.FadeOut();

        if (type != FloatyTypes.Gold && type != FloatyTypes.Gem)
        {
            Move();
        }

        if (type == FloatyTypes.SkillDamage)
        {
            Canvas.sortingOrder = 10;
        }
        else
        {
            Canvas.sortingOrder = 5;
        }
    }

    private void SetColor(FloatyTypes type)
    {
        if (type == FloatyTypes.Damage)
        {
            Text.color = Color.white;
        }
        else if (type == FloatyTypes.Success || type == FloatyTypes.Gold || type == FloatyTypes.Gem)
        {
            Text.color = Color.yellow;
        }
        else if (type == FloatyTypes.Fail || type == FloatyTypes.CritialDamage)
        {
            Text.color = Color.red;
        }
        else if (type == FloatyTypes.SkillDamage)
        {
            Text.color = EXText.lightCyan;
        }
        else if (type == FloatyTypes.CritialSkillDamage)
        {
            Text.color = Color.cyan;
        }
        else if (type == FloatyTypes.Dodge)
        {
            Text.color = Color.gray;
        }
        else if (type == FloatyTypes.Heal)
        {
            Text.color = Color.green;
        }
    }

    private void RefreshSize(FloatyTypes type)
    {
        Text.fontSize = _fontSize;

        if (_widthSize == 0)
        {
            _widthSize = Display.main.systemWidth;
        }

        if (_widthSize > 1080)
        {
            Text.fontSize *= 0.8f;
        }

        if (type is FloatyTypes.Gold or FloatyTypes.Gem)
        {
            Text.fontSize *= 0.7f;
        }
    }

    private Vector3 GetWorldPosition(Vector2 position)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);
        screenPos = new Vector3(screenPos.x - CanvasRect.localPosition.x, screenPos.y - CanvasRect.localPosition.y);

        return screenPos + Offset;
    }

    private void Move()
    {
        Text.gameObject.transform.DOMoveY(Text.gameObject.transform.position.y + 100f, AliveTime);
    }

    public void Despawn()
    {
        IsAlive = false;
        Text.gameObject.SetActive(false);
        Text.gameObject.transform.position = Vector3.zero;
    }

    private IEnumerator ProcessAlive()
    {
        yield return new WaitForSeconds(AliveTime);
        Despawn();
    }
}