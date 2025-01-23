using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoder : MonoBehaviour
{
    public Slider ProgressBar;

    private void Start()
    {
        StartCoroutine(LoadMainSceneAsync());
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
}