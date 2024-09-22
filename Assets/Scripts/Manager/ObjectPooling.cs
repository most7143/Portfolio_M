using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject Floaty;

    public List<UIFloaty> floaties;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < 10; i++)
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
        if (type == FloatyTypes.Damage)
        {
            for (int i = 0; i < floaties.Count; i++)
            {
                if (false == floaties[i].IsAlive)
                {
                    floaties[i].Spawn(position, type, text);
                    break;
                }
            }
        }
    }
}