using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    private static FXManager instance = null;

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

    public static FXManager Instance
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

    public List<FX> FXObjects;

    public void Spawn(FXNames fxName, Player player)
    {
        FX fxObject = GetFXObejct(fxName);

        if (fxObject == null)
        {
            fxObject = ResourcesManager.Instance.Load(fxName).GetComponent<FX>();
            FXObjects.Add(fxObject);
        }
        else
        {
            fxObject.gameObject.SetActive(false);
            fxObject.Activate();
        }

        fxObject.transform.SetParent(transform);

        fxObject.transform.position = SpawnRange(player, fxObject.SpawnType);
    }

    public Vector3 SpawnRange(Player player, FXSpawnTypes spawnType)
    {
        Vector3 spawnPossition = Vector3.zero;

        if (spawnType == FXSpawnTypes.Target)
        {
            return player.TargetMonster.transform.position;
        }
        else if (spawnType == FXSpawnTypes.TargetRandomRange)
        {
            return player.TargetMonster.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0, 0.5f));
        }
        else if (spawnType == FXSpawnTypes.Owner)
        {
            return player.AttackPoint.position;
        }

        return spawnPossition;
    }

    private FX GetFXObejct(FXNames fxName)
    {
        for (int i = 0; i < FXObjects.Count; i++)
        {
            if (FXObjects[i].Name == fxName && false == FXObjects[i].Alive)
            {
                return FXObjects[i];
            }
        }

        return null;
    }
}