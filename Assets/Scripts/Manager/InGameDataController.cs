using System;
using UnityEngine;

public class InGameDataController : MonoBehaviour
{
    public InGameData Data = new();

    public RectTransform GoldPoint;

    public RectTransform GemPoint;

    public void AddCurrencyAnim(CurrencyTypes type, Vector3 startPos, int value)
    {
        if (type == CurrencyTypes.Gold)
        {
            InGameManager.Instance.ObjectPool.SpawnCurrency(startPos, CurrencyTypes.Gold, GoldPoint, value);
        }
        else if (type == CurrencyTypes.Gem)
        {
            InGameManager.Instance.ObjectPool.SpawnCurrency(startPos, CurrencyTypes.Gem, GemPoint, value);
        }
    }

    public void AddCurrency(CurrencyTypes type, int value)
    {
        Player player = InGameManager.Instance.Player;

        if (type == CurrencyTypes.Gold)
        {
            int resultGold = Mathf.CeilToInt(value * player.StatSystem.GetStat(StatNames.CurrencyGainRate));

            Data.Gold += resultGold;
            Data.AccumulatedGold += resultGold;
            UIManager.Instance.PlayerInfo.RefreshCurrency(CurrencyTypes.Gold, Data.Gold);

            InGameManager.Instance.ObjectPool.SpawnFloaty(GoldPoint.position, FloatyTypes.Gold, "+" + resultGold);
        }
        else if (type == CurrencyTypes.Gem)
        {
            Data.Gem += value;
            UIManager.Instance.PlayerInfo.RefreshCurrency(CurrencyTypes.Gem, Data.Gem);
            InGameManager.Instance.ObjectPool.SpawnFloaty(GoldPoint.position, FloatyTypes.Gem, "+" + value);
        }

        EventManager<EventTypes>.Send(EventTypes.AddCurrency, type);
    }

    public void UseCurrency(CurrencyTypes type, int value)
    {
        if (type == CurrencyTypes.Gold)
        {
            Data.Gold -= value;
            UIManager.Instance.PlayerInfo.RefreshCurrency(CurrencyTypes.Gold, Data.Gold);
        }
        else if (type == CurrencyTypes.Gem)
        {
            Data.Gem -= value;
            UIManager.Instance.PlayerInfo.RefreshCurrency(CurrencyTypes.Gem, Data.Gem);
        }
        EventManager<EventTypes>.Send(EventTypes.UseCurrency, type);
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
        int currentEXP = Mathf.CeilToInt(exp * InGameManager.Instance.Player.StatSystem.GetStat(StatNames.ExpGainRate));

        Data.Experience += currentEXP;

        if (Data.NextEXP <= Data.Experience)
        {
            Data.Experience -= Data.NextEXP;

            InGameManager.Instance.Player.LevelUp();
            Data.NextEXP = GetNextEXP(InGameManager.Instance.Player.Level);
        }

        UIManager.Instance.PlayerInfo.RefreshExp();
    }

    private int GetNextEXP(int level)
    {
        double baseExp = 50;
        double currentExp = baseExp;

        for (int i = 2; i <= level; i++)
        {
            // 10레벨 단위마다 증가율 감소
            int decayStep = (i - 1) / 10;
            double growthRate = 0.50 - 0.05 * decayStep;
            growthRate = Math.Max(growthRate, 0.05); // 최소 증가율 5%

            if (i % 10 == 0)
            {
                currentExp *= 1.5;  // 10레벨마다 50% 증가
            }
            else
            {
                currentExp *= (1 + growthRate);  // 일반적인 증가
            }
        }

        return (int)Math.Round(currentExp);
    }
}