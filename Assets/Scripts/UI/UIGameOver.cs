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
        // ��ư ��Ȱ��ȭ (���� ��ư�� ������ �� ��ȯ�� ���� ����)
        Button.interactable = false;

        // �� ��ȯ
        SceneManager.LoadScene(sceneName);
    }
}