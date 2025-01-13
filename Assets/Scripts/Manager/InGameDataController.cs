using UnityEngine;

public class InGameDataController : MonoBehaviour
{
    public InGameData Data = new();

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

    public void AddExp(int exp)
    {
        Data.Experience += exp;
    }
}