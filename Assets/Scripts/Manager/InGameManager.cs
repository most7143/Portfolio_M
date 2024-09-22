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

    public Player Player;
    public Monster Monster;

    public ObjectPooling ObjectPool;

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void SpawnMonster()
    {
    }
}