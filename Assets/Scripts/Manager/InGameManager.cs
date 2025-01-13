using Assets.Scripts.Manager;
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

    public StageManager StageManager;
    public MonsterSpanwer MonsterSpanwer;

    public InGameDataController Controller;
    public ObjectPooling ObjectPool;

    [HideInInspector]
    public Monster Monster
    { get { return MonsterSpanwer.SpawnMonster; } }

    private void Start()
    {
        MonsterSpanwer.Spawn(CharacterNames.BoneWorm);
        StageManager.Spawn(StageNames.Forest);
    }

    public void Update()
    {
        if (MonsterSpanwer.SpawnMonster != null)
        {
            IsBattle = true;
        }
    }

    public void RefreshStage(int monsterLevel)
    {
        StageManager.ChangeStage(monsterLevel);
    }
}