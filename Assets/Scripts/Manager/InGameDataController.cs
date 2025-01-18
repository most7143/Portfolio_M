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
        Data.Gold += gold;

        UIManager.Instance.PlayerInfo.RefreshGold(Data.Gold);
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
        Data.Experience += exp;

        if (Data.NextEXP <= Data.Experience)
        {
            Data.Experience = exp - Data.NextEXP;

            InGameManager.Instance.Player.LevelUp();
            Data.NextEXP *= 1.2f;
        }

        UIManager.Instance.PlayerInfo.RefreshExp();
    }
}