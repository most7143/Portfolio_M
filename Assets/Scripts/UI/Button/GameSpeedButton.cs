using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedButton : MonoBehaviour
{
    private int Speed = 1;

    public Button XButton;

    public TextMeshProUGUI Text;

    private void Start()
    {
        XButton.onClick.AddListener(() => OnClick());
    }

    private void OnClick()
    {
        switch (Speed)
        {
            case 1:
                {
                    Speed = 2;
                }
                break;

            case 2:
                {
                    Speed = 1;
                }
                break;
        }

        Time.timeScale = Speed;

        Text.SetText("X" + Speed);
    }
}