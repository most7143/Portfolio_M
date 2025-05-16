using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject Floaty;

    public GameObject Currency;

    private List<UIFloaty> floaties = new();

    private List<CurrencyObject> Currencies = new();

    private Dictionary<ProjectileNames, Queue<Projectile>> Projectiles = new();

    public int InitCount = 30;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < InitCount; i++)
        {
            InitFloaty();
        }

        for (int i = 0; i < InitCount; i++)
        {
            InitCurrency();
        }
    }

    private CurrencyObject InitCurrency()
    {
        GameObject currencyObj = Instantiate(Currency);

        CurrencyObject currency = currencyObj.GetComponent<CurrencyObject>();

        currency.transform.SetParent(transform);
        currency.Init();

        Currencies.Add(currency);

        return currency;
    }

    private UIFloaty InitFloaty()
    {
        GameObject floatyObj = Instantiate(Floaty);

        UIFloaty floaty = floatyObj.GetComponent<UIFloaty>();

        floaty.Init();

        floaty.transform.SetParent(transform);

        floaties.Add(floaty);

        return floaty;
    }

    public void SpawnFloaty(Vector2 position, FloatyTypes type, string text)
    {
        UIFloaty floaty = GetFolaty();

        if (floaty == null)
            return;

        floaty.Spawn(position, type, text);
    }

    public void SpawnCurrency(Vector3 startPos, CurrencyTypes type, RectTransform endRect, float value)
    {
        CurrencyObject currency = GetCurrency();

        currency.Spawn(type, startPos, endRect, value);
    }

    public void SpawnProejctile()
    {
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

        return InitFloaty();
    }

    private CurrencyObject GetCurrency()
    {
        for (int i = 0; i < Currencies.Count; i++)
        {
            if (false == Currencies[i].IsAlive)
            {
                return Currencies[i];
            }
        }

        return InitCurrency();
    }

    public Projectile GetProjectile(ProjectileNames name)
    {
        if (!Projectiles.ContainsKey(name))
            Projectiles[name] = new Queue<Projectile>();

        Queue<Projectile> queue = Projectiles[name];

        if (queue.Count > 0)
        {
            Projectile proj = queue.Dequeue();
            proj.gameObject.SetActive(true);
            return proj;
        }

        GameObject projectileObj = ResourcesManager.Instance.Load(name);
        if (projectileObj != null)
        {
            Projectile newProj = projectileObj.GetComponent<Projectile>();
            return newProj;
        }

        Debug.LogError($"[{name}] 프리팹이 등록되어 있지 않습니다.");
        return null;
    }

    public void ReturnProjectile(ProjectileNames name, Projectile proj)
    {
        if (Projectiles.ContainsKey(name))
        {
            Projectiles[name].Enqueue(proj);
            proj.gameObject.SetActive(false);
        }
    }
}