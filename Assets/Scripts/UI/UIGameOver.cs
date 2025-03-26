using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    public CanvasGroup Group;

    public TextMeshProUGUI MaxKillMonsterLevel;

    public Button Button;

    private void Start()
    {
        Button.onClick.AddListener(() => LoadScene("TitleScene"));

        Group.alpha = 0f;
        Group.interactable = false;
    }

    public void Show()
    {
        Group.alpha = 1f;
        Group.interactable = true;
    }

    public void LoadScene(string sceneName)
    {
        // 버튼 비활성화 (이제 버튼을 눌러도 씬 전환이 되지 않음)
        Button.interactable = false;

        // 씬 전환
        SceneManager.LoadScene(sceneName);
    }
}