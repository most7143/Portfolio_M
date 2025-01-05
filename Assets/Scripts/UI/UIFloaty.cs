using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public enum FloatyTypes
{
    None,
    Damage,
}

public class UIFloaty : MonoBehaviour
{
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
        Text.gameObject.SetActive(true);
        StartCoroutine(ProcessAlive());
        Rect.anchoredPosition = GetPosition(position);

        Move();
    }

    private Vector3 GetPosition(Vector2 position)
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