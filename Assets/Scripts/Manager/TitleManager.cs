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
        // ��ư ��Ȱ��ȭ (���� ��ư�� ������ �� ��ȯ�� ���� ����)
        StartButton.interactable = false;

        // �� ��ȯ
        SceneManager.LoadScene(sceneName);
    }
}