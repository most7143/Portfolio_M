using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    public TextMeshProUGUI MaxKillMonsterLevel;

    public Button Button;

    private void Start()
    {
        Button.onClick.AddListener(() => LoadScene("TitleScene"));
        Button.gameObject.SetActive(false);
        MaxKillMonsterLevel.gameObject.SetActive(false);
    }

    public void Show()
    {
        MaxKillMonsterLevel.gameObject.SetActive(true);
        Button.gameObject.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        // ��ư ��Ȱ��ȭ (���� ��ư�� ������ �� ��ȯ�� ���� ����)
        Button.interactable = false;

        // �� ��ȯ
        SceneManager.LoadScene(sceneName);
    }
}