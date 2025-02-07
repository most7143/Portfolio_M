using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public List<FX> FXObjects;

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void Spawn(FXNames fxName)
    {
    }

    private bool IsInstance(FXNames fxName)
    {
        List<FX> fxList = FXObjects.FindAll(p => p.Name == fxName);

        if (fxList.Count > 0)
        {
        }

        return false;
    }
}