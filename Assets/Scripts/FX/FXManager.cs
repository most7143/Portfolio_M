using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public List<FX> FXObjects;

    public void Spawn(FXNames fxName, Vector3 position)
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

        fxObject.transform.position = SpawnRange(transform.position, fxObject.SpawnType);
    }

    public Vector3 SpawnRange(Vector3 targetPosition, FXSpawnTypes spawnType)
    {
        Vector3 spawnPossition = Vector3.zero;

        if (spawnType == FXSpawnTypes.Target)
        {
            return targetPosition;
        }
        else if (spawnType == FXSpawnTypes.TargetRandomRange)
        {
            return targetPosition + new Vector3(Random.Range(0, 1f), Random.Range(0, 1f));
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