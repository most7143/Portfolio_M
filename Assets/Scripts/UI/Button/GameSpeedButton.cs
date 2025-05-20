using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedButton : MonoBehaviour
{
    public Button XButton;

    public TextMeshProUGUI Text;

    private void Start()
    {
        Time.timeScale = InGameManager.Instance.GameSpeed;

        Text.SetText("X" + InGameManager.Instance.GameSpeed);

        XButton.onClick.AddListener(() => OnClick());
    }

    private void OnClick()
    {
        switch (InGameManager.Instance.GameSpeed)
        {
            case 1f:
                {
                    InGameManager.Instance.GameSpeed = 1.5f;
                }
                break;

            case 1.5f:
                {
                    InGameManager.Instance.GameSpeed = 1f;
                }
                break;
        }

        Time.timeScale = InGameManager.Instance.GameSpeed;

        Text.SetText("X" + InGameManager.Instance.GameSpeed);
    }
}