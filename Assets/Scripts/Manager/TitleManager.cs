using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public XButton StartButton;
    public XButton StartDescriptionButton;
    public XButton MemoryButton;
    public TextMeshProUGUI KillMonsterText;

    private void OnEnable()
    {
        EventManager<EventTypes>.Register(EventTypes.RefreshMemory, RefreshMemoryButton);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister(EventTypes.RefreshMemory, RefreshMemoryButton);
    }

    private void Start()
    {
        Time.timeScale = 1f;
        StartButton.OnExecute = LoadScene;
        StartDescriptionButton.OnExecute = ClickDescrioption;
        StartDescriptionButton.IsPressClick = false;
        MemoryButton.OnExecute = ClickMemory;
        MemoryButton.IsPressClick = false;

        if (PlayerPrefs.GetInt("KillMonster") > 0)
        {
            KillMonsterText.SetText("최대 처치 몬스터 레벨 " + PlayerPrefs.GetInt("KillMonster").ToString());
        }

        if (PlayerPrefs.GetInt("MemoryMaxPoint") > 0)
        {
            MemoryButton.interactable = true;
            MemoryButton.image.color = new Color(255f, 255f, 255f, 1f);
        }
        else
        {
            MemoryButton.interactable = false;
            MemoryButton.image.color = new Color(255f, 255f, 255f, 0.2f);
        }

        RefreshMemoryData();
        RefreshMemoryButton();
    }

    public void LoadScene()
    {
        StartButton.interactable = false;

        SceneManager.LoadScene("LoadingScene");
    }

    public void ClickDescrioption()
    {
        if (UIPopupManager.Instance.Name != UIPopupNames.GameDescription)
        {
            UIPopupManager.Instance.Spawn(UIPopupNames.GameDescription);
        }
        else
        {
            UIPopupManager.Instance.Despawn();
        }
    }

    public void ClickMemory()
    {
        if (UIPopupManager.Instance.Name != UIPopupNames.Memory)
        {
            UIPopupManager.Instance.Spawn(UIPopupNames.Memory);
        }
        else
        {
            UIPopupManager.Instance.Despawn();
        }
    }

    private void RefreshMemoryButton()
    {
        if (PlayerPrefs.GetInt("MemoryPoint") > 0)
        {
            MemoryButton.image.sprite = ResourcesManager.Instance.LoadSprite("TitleMemoryButtonBackground_Activate");
        }
        else
        {
            MemoryButton.image.sprite = ResourcesManager.Instance.LoadSprite("TitleMemoryButtonBackground_Normal");
        }
    }

    private void RefreshMemoryData()
    {
        UIPopupManager.Instance.Spawn(UIPopupNames.Memory, true);
        UIPopupManager.Instance.Despawn();
    }
}