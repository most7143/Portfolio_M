using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject Floaty;

    public List<UIFloaty> floaties;

    public int InitCount = 15;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < InitCount; i++)
        {
            GameObject floatyObj = Instantiate(Floaty);

            UIFloaty floaty = floatyObj.GetComponent<UIFloaty>();

            floaty.Init();

            floaty.transform.SetParent(transform);

            floaties.Add(floaty);
        }
    }

    public void SpawnFloaty(Vector2 position, FloatyTypes type, string text)
    {
        UIFloaty floaty = GetFolaty();

        if (floaty == null)
            return;

        floaty.Spawn(position, type, text);
    }

    private UIFloaty GetFolaty()
    {
        for (int i = 0; i < floaties.Count; i++)
        {
            if (false == floaties[i].IsAlive)
            {
                return floaties[i];
            }
        }

        return null;
    }
}