using DG.Tweening;
using UnityEngine;

public class CurrencyObject : MonoBehaviour
{
    public bool IsAlive;
    public RectTransform targetUI; // �� �ؽ�Ʈ ��ġ
    public float moveDuration = 0.8f;
    public float arcHeight = 100f;

    public SpriteRenderer Icon;

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

        // 1. ���� ��ġ�� �״�� ���
        Vector3 startWorld = worldPosition;
        transform.position = startWorld;

        // 2. targetUI�� ȭ�� ��ǥ ��������
        Vector3 targetScreenPos = RectTransformUtility.WorldToScreenPoint(UIManager.Instance.HUDCanvas.worldCamera, targetUI.position + (Vector3)Offset);

        // 3. �� ȭ�� ��ǥ�� ���� ��ǥ�� ��ȯ (���� ī�޶� ����)
        Vector3 endWorld = Camera.main.ScreenToWorldPoint(new Vector3(targetScreenPos.x, targetScreenPos.y, Camera.main.nearClipPlane + 1f));
        endWorld.z = 0f; // ��� ������ �����̰� �ϱ� ���� Z ����

        // 4. ���� ����
        float randomArcHeight = arcHeight * Random.Range(0.8f, 1.2f);

        DOTween.To(() => 0f, t =>
        {
            Vector3 pos = Vector3.Lerp(startWorld, endWorld, t);
            pos.y += Mathf.Sin(t * Mathf.PI) * randomArcHeight;
            transform.position = pos;
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

        EventManager<EventTypes>.Send(EventTypes.AddCurrency, Type);

        gameObject.SetActive(false);
    }
}