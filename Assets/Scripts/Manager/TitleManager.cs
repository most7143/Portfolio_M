using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Button StartButton;
    public TextMeshProUGUI KillMonsterText;

    private void Start()
    {
        Time.timeScale = 1f;
        StartButton.onClick.AddListener(() => LoadScene("LoadingScene"));

        if (PlayerPrefs.GetInt("KillMonster") > 0)
        {
            KillMonsterText.SetText("�ִ� óġ ���� ���� " + PlayerPrefs.GetInt("KillMonster").ToString());
        }
    }

    public void LoadScene(string sceneName)
    {
        // ��ư ��Ȱ��ȭ (���� ��ư�� ������ �� ��ȯ�� ���� ����)
        StartButton.interactable = false;

        // �� ��ȯ
        SceneManager.LoadScene(sceneName);
    }
}