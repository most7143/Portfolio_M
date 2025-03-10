﻿using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public enum FloatyTypes
{
    None,
    Damage,
    CritialDamage,
    SkillDamage,
    Success,
    Fail,
    EXP,
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

    public void Init()
    {
        Text.gameObject.SetActive(false);
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

        if (type == FloatyTypes.Damage || type == FloatyTypes.CritialDamage || type == FloatyTypes.SkillDamage || type == FloatyTypes.EXP)
        {
            Rect.anchoredPosition = GetWorldPosition(position);
        }
        else
        {
            Rect.position = position;
        }
        Fade.FadeOut();
        Move();

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
        else if (type == FloatyTypes.Success || type == FloatyTypes.EXP)
        {
            Text.color = Color.yellow;
        }
        else if (type == FloatyTypes.Fail || type == FloatyTypes.CritialDamage)
        {
            Text.color = Color.red;
        }
        else if (type == FloatyTypes.SkillDamage)
        {
            Text.color = Color.cyan;
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