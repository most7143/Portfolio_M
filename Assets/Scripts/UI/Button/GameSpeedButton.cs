using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedButton : MonoBehaviour
{
    public Button XButton;

    public TextMeshProUGUI Text;

    private void Start()
    {
        XButton.onClick.AddListener(() => OnClick());
    }

    private void OnClick()
    {
        switch (InGameManager.Instance.GameSpeed)
        {
            case 1:
                {
                    InGameManager.Instance.GameSpeed = 2f;
                }
                break;

            case 2:
                {
                    InGameManager.Instance.GameSpeed = 1f;
                }
                break;
        }

        EventManager<EventTypes>.Send(EventTypes.RefreshAttackSpeed);

        Text.SetText("X" + InGameManager.Instance.GameSpeed);
    }
}