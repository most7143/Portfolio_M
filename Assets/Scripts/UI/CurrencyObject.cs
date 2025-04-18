using DG.Tweening;
using UnityEngine;

public class CurrencyObject : MonoBehaviour
{
    public bool IsAlive;
    public RectTransform CanvasRect;
    public RectTransform coinRect;
    public RectTransform targetUI; // 돈 텍스트 위치
    public float moveDuration = 0.8f;
    public float arcHeight = 100f;

    public CurrencyTypes Type = CurrencyTypes.Gold;
    public float Value;

    public void Init()
    {
        gameObject.SetActive(false);
    }

    public void Spawn(Vector3 worldPosition, RectTransform traget, float value)
    {
        gameObject.SetActive(true);
        IsAlive = true;
        targetUI = traget;
        Value = value;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
        screenPos = new Vector3(screenPos.x - CanvasRect.localPosition.x, screenPos.y - CanvasRect.localPosition.y);

        coinRect.anchoredPosition = screenPos;
        Vector2 start = screenPos;

        Vector2 end = RectTransformUtility.WorldToScreenPoint(null, targetUI.position);
        end = new Vector3(end.x - CanvasRect.localPosition.x, end.y - CanvasRect.localPosition.y);

        float randomArcHeight = arcHeight * Random.Range(0.8f, 1.2f);

        DOTween.To(() => 0f, t =>
        {
            Vector2 pos = Vector2.Lerp(start, end, t);

            pos.y += Mathf.Sin(t * Mathf.PI) * randomArcHeight;

            coinRect.anchoredPosition = pos;
        }, 1f, moveDuration).SetEase(Ease.Linear).OnComplete(() => OnArrive());
    }

    private void OnArrive()
    {
        IsAlive = false;
        gameObject.SetActive(false);

        int resultGold = Mathf.CeilToInt(Value * InGameManager.Instance.Player.StatSystem.GetStat(StatNames.CurrencyGainRate));

        InGameManager.Instance.Controller.Data.Gold += resultGold;
        InGameManager.Instance.Controller.Data.AccumulatedGold += resultGold;

        InGameManager.Instance.ObjectPool.SpawnFloaty(targetUI.position, FloatyTypes.Gold, "+" + Value + "G");
        UIManager.Instance.PlayerInfo.RefreshGold(InGameManager.Instance.Controller.Data.Gold);
        EventManager<EventTypes>.Send(EventTypes.AddCurrency);
    }
}