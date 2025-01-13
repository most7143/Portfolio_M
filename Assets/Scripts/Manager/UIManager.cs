using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static UIManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public UIStageInfo StageInfo;
    public UIMonsterInfo MonsterInfo;
    public UIPlayerInfo PlayerInfo;

    private void Start()
    {
        PlayerInfo.Setup(InGameManager.Instance.Player);
    }
}