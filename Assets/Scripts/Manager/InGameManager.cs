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

    public bool IsBattle;
    public int StageLevel = 1;
    public Player Player;

    public MonsterSpanwer MonsterSpanwer;

    public InGameDataController Controller;
    public ObjectPooling ObjectPool;

    public UIMonsterInfo MonsterInfo;
    public UIPlayerInfo PlayerInfo;

    [HideInInspector]
    public Monster Monster
    { get { return MonsterSpanwer.SpawnMonster; } }

    public void StageClear()
    {
    }

    private void Start()
    {
        PlayerInfo.Setup(Player);
        MonsterSpanwer.Spawn(CharacterNames.BoneWorm);
    }

    public void Update()
    {
        if (MonsterSpanwer.SpawnMonster != null)
        {
            IsBattle = true;
        }
    }

    public void SpawnMonster()
    {
        MonsterInfo.SetInfo(Monster);
    }
}