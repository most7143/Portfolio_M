using TMPro;
using UnityEngine;

public class UICharacterInfo : MonoBehaviour
{
    public TextMeshProUGUI RankText;
    public TextMeshProUGUI ClassText;
    public Sprite ClassIcon;

    private void OnEnable()
    {
        EventManager<EventTypes>.Register<int>(EventTypes.LevelUp, RefreshRank);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister<int>(EventTypes.LevelUp, RefreshRank);
    }

    public void Activate()
    {
        Player player = InGameManager.Instance.Player;
        RefreshRank(player.Level);
    }

    public void RefreshRank(int level)
    {
        RankText.SetText("Rank " + level);
    }
}