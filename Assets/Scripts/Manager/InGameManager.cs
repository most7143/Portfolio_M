using UnityEngine;

public class InGameManager : MonoBehaviour
{
    private static InGameManager instance = null;

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

    public static InGameManager Instance
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

    public int StageLevel = 1;
    public Player Player;
    public Monster Monster;

    public InGameDataController Controller;
    public ObjectPooling ObjectPool;

    public UIMonsterInfo MonsterInfo;
    public UIPlayerInfo PlayerInfo;

    public void StageClear()
    {
    }

    private void Start()
    {
        PlayerInfo.Setup(Player);
    }

    public void SpawnMonster()
    {
        MonsterInfo.SetInfo(Monster);
    }
}