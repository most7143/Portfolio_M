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

    public UIFade Fade;

    public float GameSpeed
    {
        get
        {
            if (PlayerPrefs.GetFloat("GameSpeed") < 1)
            {
                PlayerPrefs.SetFloat("GameSpeed", 1);
            }

            return PlayerPrefs.GetFloat("GameSpeed");
        }

        set { PlayerPrefs.SetFloat("GameSpeed", value); }
    }

    [HideInInspector]
    public Monster Monster
    { get { return MonsterSpanwer.SpawnMonster; } }

    private float _originGameSpeed = 1;

    private void Start()
    {
        OutGameManager.Instance.SetResolution();

        BattleStart();

        Fade.FadeOut();
    }

    private void BattleStart()
    {
        Player.Init();

        MonsterSpanwer.Spawn(CharacterNames.SlimeGreen);
        StageManager.Spawn(StageNames.Forest);

        Player.StartAttack();
        Monster.StartAttack();

        IsBattle = true;
    }

    public void StageFail()
    {
        IsBattle = false;
        Time.timeScale = 1f;
        UIHandler.FadeOut();
        StartCoroutine(GameOverProcess());
        SaveData();
    }

    public void SaveData()
    {
        if (PlayerPrefs.GetInt("KillMonster") < MonsterSpanwer.Level - 1)
        {
            PlayerPrefs.SetInt("KillMonster", MonsterSpanwer.Level - 1);
        }

        OutGameManager.Instance.SaveChallengeData();
        PlayerPrefs.Save();
    }

    private IEnumerator GameOverProcess()
    {
        yield return new WaitForSeconds(1f);
        GameOver.MaxKillMonsterLevel.SetText("처치 몬스터 레벨 : {0} ", MonsterSpanwer.Level - 1);

        GameOver.Show();
    }

    public void RefreshStage(int monsterLevel)
    {
        StageManager.ChangeStage(monsterLevel);
    }

    public void ContinueBattle()
    {
        Time.timeScale = GameSpeed;
    }

    public void PauseBattle()
    {
        Time.timeScale = 0;
    }
}