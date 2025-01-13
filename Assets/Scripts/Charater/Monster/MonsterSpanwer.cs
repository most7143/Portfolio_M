using UnityEngine;

public class MonsterSpanwer : MonoBehaviour
{
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

            UIManager.Instance.MonsterInfo.Refresh(SpawnMonster);
        }
    }

    public void Respawn(CharacterNames chareacterName)
    {
        Deswpawn();
        Spawn(chareacterName);
    }

    public void Deswpawn()
    {
        if (SpawnMonster != null)
        {
            Destroy(SpawnMonster.gameObject);
        }
    }
}