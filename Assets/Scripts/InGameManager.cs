using UnityEngine;

public class InGameManager : MonoBehaviour
{ //���ӸŴ����� �ν��Ͻ��� ��� ��������(static ���������� �����ϱ� ���� ����������� �ϰڴ�).
    //�� ���� ������ ���ӸŴ��� �ν��Ͻ��� �� instance�� ��� �༮�� �����ϰ� �� ���̴�.
    //������ ���� private����.
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