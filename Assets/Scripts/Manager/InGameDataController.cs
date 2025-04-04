using UnityEngine;

public class InGameDataController : MonoBehaviour
{
    public InGameData Data = new();

    public void Setup()
    {
        Data.Experience = 0;
        Data.NextEXP = 100;
    }

    public void AddGold(int gold)
    {
        Player player = InGameManager.Instance.Player;

        int resultGold = Mathf.CeilToInt(gold * player.StatSystem.GetStat(StatNames.CurrencyGainRate));

        Data.Gold += resultGold;

        UIManager.Instance.PlayerInfo.RefreshGold(Data.Gold);

        InGameManager.Instance.ObjectPool.SpawnFloaty(InGameManager.Instance.Player.transform.position, FloatyTypes.Gold, "+" + resultGold + "G");
    }

    public void UseGold(int gold)
    {
        if (Data.Gold >= gold)
        {
            Data.Gold -= gold;

            UIManager.Instance.PlayerInfo.RefreshGold(Data.Gold);
        }
    }

    public bool TryUsingGold(int gold)
    {
        return Data.Gold >= gold;
    }

    public void AddExp(float exp)
    {
        float currentEXP = Mathf.FloorToInt(exp);

        Data.Experience += currentEXP;

        if (Data.NextEXP <= Data.Experience)
        {
            Data.Experience -= Data.NextEXP;

            InGameManager.Instance.Player.LevelUp();
            Data.NextEXP *= 1.3f;
        }

        UIManager.Instance.PlayerInfo.RefreshExp();
    }
}