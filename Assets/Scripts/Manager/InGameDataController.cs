using UnityEngine;

public class InGameDataController : MonoBehaviour
{
    public InGameData Data = new();

    public RectTransform GoldPoint;

    public void AddCurrencyAnim(CurrencyTypes type, Vector3 startPos, int value)
    {
        InGameManager.Instance.ObjectPool.SpawnCurrency(startPos, CurrencyTypes.Gold, GoldPoint, value);
    }

    public void AddCurrency(CurrencyTypes type, int value)
    {
        Player player = InGameManager.Instance.Player;

        if (type == CurrencyTypes.Gold)
        {
            int resultGold = Mathf.CeilToInt(value * player.StatSystem.GetStat(StatNames.CurrencyGainRate));

            Data.Gold += resultGold;
            Data.AccumulatedGold += resultGold;
            UIManager.Instance.PlayerInfo.RefreshGold(Data.Gold);

            InGameManager.Instance.ObjectPool.SpawnFloaty(GoldPoint.position, FloatyTypes.Gold, "+" + resultGold + "G");
        }
        else if (type == CurrencyTypes.Gem)
        {
            Data.Gem += value;
        }

        EventManager<EventTypes>.Send(EventTypes.AddCurrency);
    }

    public void UseCurrency(CurrencyTypes type, int value)
    {
        if (type == CurrencyTypes.Gold)
        {
            Data.Gold -= value;
            UIManager.Instance.PlayerInfo.RefreshGold(Data.Gold);
        }
        else if (type == CurrencyTypes.Gem)
        {
            Data.Gem -= value;
        }

        EventManager<EventTypes>.Send(EventTypes.UseCurrency);
    }

    public bool TryUsingCurrency(CurrencyTypes type, int value)
    {
        if (type == CurrencyTypes.Gold)
        {
            return Data.Gold >= value;
        }
        else if (type == CurrencyTypes.Gem)
        {
            return Data.Gem >= value;
        }

        return false;
    }

    public void AddExp(float exp)
    {
        float currentEXP = Mathf.FloorToInt(exp);

        Data.Experience += currentEXP;

        if (Data.NextEXP <= Data.Experience)
        {
            Data.Experience -= Data.NextEXP;

            InGameManager.Instance.Player.LevelUp();
            Data.NextEXP *= 1.1f;
        }

        UIManager.Instance.PlayerInfo.RefreshExp();
    }
}