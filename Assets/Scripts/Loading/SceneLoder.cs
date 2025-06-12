using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoder : MonoBehaviour
{
    public Slider ProgressBar;

    public TextMeshProUGUI TipText;

    private void Start()
    {
        OutGameManager.Instance.SetResolution();

        StartCoroutine(LoadMainSceneAsync());

        SetText();
    }

    private IEnumerator LoadMainSceneAsync()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("MainScene");
        asyncOperation.allowSceneActivation = false;

        float timeElapsed = 0f;
        bool isSceneReady = false;

        while (!asyncOperation.isDone)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed < 3f)
            {
                ProgressBar.value = Mathf.Lerp(0, 1, timeElapsed / 3f);
            }
            else
            {
                ProgressBar.value = Mathf.Clamp01(asyncOperation.progress / 0.9f);

                if (asyncOperation.progress >= 0.9f && !isSceneReady)
                {
                    asyncOperation.allowSceneActivation = true;
                    isSceneReady = true;
                }
            }

            yield return null;
        }
    }

    private void SetText()
    {
        List<string> tips = new();

        string str1 = "���������� �޼��ϸ� �ҷ��� �ɷ�ġ�� ȹ�� �մϴ�.";
        string str2 = "����Ʈ ���� óġ �� �뷮�� ��ȭ�� ȹ�� �մϴ�.";
        string str3 = "óġ�ߴ� ���Ϳ� ����� ��� ����Ʈ�� ȹ���ϰ�, \n Ÿ��Ʋ ȭ�鿡�� �ɷ�ġ�� ���� �� �ֽ��ϴ�.";

        tips.Add(str1);
        tips.Add(str2);
        tips.Add(str3);

        TipText.SetText(tips[Random.Range(0, tips.Count)]);
    }
}