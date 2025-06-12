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

        string str1 = "도전과제를 달성하면 소량의 능력치를 획득 합니다.";
        string str2 = "엘리트 몬스터 처치 시 대량의 재화를 획득 합니다.";
        string str3 = "처치했던 몬스터에 비례해 기억 포인트를 획득하고, \n 타이틀 화면에서 능력치를 찍을 수 있습니다.";

        tips.Add(str1);
        tips.Add(str2);
        tips.Add(str3);

        TipText.SetText(tips[Random.Range(0, tips.Count)]);
    }
}