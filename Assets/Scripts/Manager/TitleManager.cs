using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Button StartButton;

    private void Start()
    {
        StartButton.onClick.AddListener(() => LoadScene("MainScene"));
    }

    public void LoadScene(string sceneName)
    {
        // 버튼 비활성화 (이제 버튼을 눌러도 씬 전환이 되지 않음)
        StartButton.interactable = false;

        // 씬 전환
        SceneManager.LoadScene(sceneName);
    }
}