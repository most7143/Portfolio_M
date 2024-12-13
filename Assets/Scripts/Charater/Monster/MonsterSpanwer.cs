using UnityEngine;

public class MonsterSpanwer : MonoBehaviour
{
    [HideInInspector]
    public Monster SpawnMonster;

    public Transform SpawnPoint;

    public void Spawn(CharacterNames chareacterName)
    {
        GameObject monster = Instantiate(Resources.Load<GameObject>("Prefabs/Monster/" + chareacterName));
        if (monster != null)
        {
            SpawnMonster = monster.GetComponent<Monster>();

            SpawnMonster.transform.SetParent(SpawnPoint);
            SpawnMonster.transform.localPosition = Vector3.zero;

            InGameManager.Instance.MonsterInfo.Refresh(SpawnMonster);
        }
    }

    public void Deswpawn()
    {
        if (SpawnMonster != null)
        {
            Destroy(SpawnMonster);
        }
    }
}