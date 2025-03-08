using Assets.Scripts.Manager;
using Assets.Scripts.UI;
using System.Collections;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    private static InGameManager instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
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

    public UIGameOver GameOver;

    [HideInInspector]
    public Monster Monster
    { get { return MonsterSpanwer.SpawnMonster; } }

    private void Start()
    {
        BattleStart();
    }

    private void BattleStart()
    {
        IsBattle = true;

        MonsterSpanwer.Spawn(CharacterNames.SlimeGreen);
        StageManager.Spawn(StageNames.Forest);

        Player.StartAttack();
    }

    public void StageFail()
    {
        IsBattle = false;
        Time.timeScale = 1f;
        UIHandler.FadeOut();
        StartCoroutine(GameOverProcess());
    }

    private IEnumerator GameOverProcess()
    {
        yield return new WaitForSeconds(1f);
        GameOver.MaxKillMonsterLevel.SetText("최대 처치 몬스터 레벨 : {0} ", MonsterSpanwer.Level);

        GameOver.Show();
    }

    public void RefreshStage(int monsterLevel)
    {
        StageManager.ChangeStage(monsterLevel);
    }
}