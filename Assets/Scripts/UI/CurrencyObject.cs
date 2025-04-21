using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyObject : MonoBehaviour
{
    public bool IsAlive;
    public RectTransform CanvasRect;
    public RectTransform coinRect;
    public RectTransform targetUI; // 돈 텍스트 위치
    public float moveDuration = 0.8f;
    public float arcHeight = 100f;

    public Image Icon;

    public CurrencyTypes Type = CurrencyTypes.Gold;

    public float Value;
    public Vector3 Offset;

    public void Init()
    {
        gameObject.SetActive(false);
    }

    public void Spawn(CurrencyTypes type, Vector3 worldPosition, RectTransform target, float value)
    {
        Type = type;

        Icon.sprite = ResourcesManager.Instance.LoadSprite("Icon_" + Type.ToString());

        gameObject.SetActive(true);
        IsAlive = true;
        targetUI = target;
        Value = value;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
        screenPos = new Vector3(screenPos.x - CanvasRect.localPosition.x, screenPos.y - CanvasRect.localPosition.y);

        coinRect.anchoredPosition = screenPos;
        Vector2 start = screenPos;

        Vector2 end = RectTransformUtility.WorldToScreenPoint(null, targetUI.position + Offset);
        end = new Vector3(end.x - CanvasRect.localPosition.x, end.y - CanvasRect.localPosition.y);

        float randomArcHeight = arcHeight * Random.Range(0.8f, 1.2f);

        DOTween.To(() => 0f, t =>
        {
            Vector2 pos = Vector2.Lerp(start, end, t);

            pos.y += Mathf.Sin(t * Mathf.PI) * randomArcHeight;

            coinRect.anchoredPosition = pos;
        }, 1f, moveDuration).SetEase(Ease.Linear).OnComplete(() => OnArrive());
    }

    private void SetSprite()
    {
        Icon.sprite = ResourcesManager.Instance.LoadSprite("Icon_" + Type.ToString());
    }

    private void OnArrive()
    {
        IsAlive = false;

        if (Type == CurrencyTypes.Gold)
        {
            int resultGold = Mathf.CeilToInt(Value * InGameManager.Instance.Player.StatSystem.GetStat(StatNames.CurrencyGainRate));
            InGameManager.Instance.Controller.Data.Gold += resultGold;
            InGameManager.Instance.Controller.Data.AccumulatedGold += resultGold;

            InGameManager.Instance.ObjectPool.SpawnFloaty(targetUI.position, FloatyTypes.Gold, "+" + resultGold);
            UIManager.Instance.PlayerInfo.RefreshCurrency(CurrencyTypes.Gold, InGameManager.Instance.Controller.Data.Gold);
        }
        else if (Type == CurrencyTypes.Gem)
        {
            InGameManager.Instance.Controller.Data.Gem += (int)Value;
            InGameManager.Instance.ObjectPool.SpawnFloaty(targetUI.position, FloatyTypes.Gem, "+" + Value);
            UIManager.Instance.PlayerInfo.RefreshCurrency(CurrencyTypes.Gem, InGameManager.Instance.Controller.Data.Gem);
        }

        EventManager<EventTypes>.Send(EventTypes.AddCurrency);

        gameObject.SetActive(false);
    }
}