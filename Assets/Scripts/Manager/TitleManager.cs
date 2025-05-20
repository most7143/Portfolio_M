using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public XButton StartButton;
    public XButton StartDescriptionButton;
    public TextMeshProUGUI KillMonsterText;

    private bool _descriptionPopup;

    private void Start()
    {
        Time.timeScale = 1f;
        StartButton.OnExecute = LoadScene;
        StartDescriptionButton.OnExecute = ClickDescrioption;

        if (PlayerPrefs.GetInt("KillMonster") > 0)
        {
            KillMonsterText.SetText("최대 처치 몬스터 레벨 " + PlayerPrefs.GetInt("KillMonster").ToString());
        }
    }

    public void LoadScene()
    {
        StartButton.interactable = false;

        SceneManager.LoadScene("LoadingScene");
    }

    public void ClickDescrioption()
    {
        if (false == _descriptionPopup)
        {
            UIPopupManager.Instance.Spawn(UIPopupNames.GameDescription);
        }
        else
        {
            UIPopupManager.Instance.Despawn();
        }

        _descriptionPopup = !_descriptionPopup;
    }
}