using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    private static ResourcesManager instance = null;

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

    public static ResourcesManager Instance
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

    public GameObject Load(UINames uiName)
    {
        GameObject ui = Instantiate(Resources.Load<GameObject>("Prefabs/UI/" + uiName.ToString()));

        if (ui != null)
        {
            return ui;
        }
        return null;
    }

    public GameObject Load(UIDetailsNames uiName)
    {
        GameObject ui = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Details/" + string.Format("UI{0}Details", uiName.ToString())));

        if (ui != null)
        {
            return ui;
        }
        return null;
    }

    public GameObject Load(UIPopupNames uiName)
    {
        GameObject ui = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Popup/" + string.Format("UI{0}Popup", uiName.ToString())));

        if (ui != null)
        {
            return ui;
        }
        return null;
    }

    public GameObject Load(BuffNames buffName)
    {
        GameObject ui = Instantiate(Resources.Load<GameObject>("Prefabs/Buff/Buff_" + buffName.ToString()));

        if (ui != null)
        {
            return ui;
        }
        return null;
    }

    public GameObject Load(ProjectileNames name)
    {
        GameObject ui = Instantiate(Resources.Load<GameObject>("Prefabs/Projectile/Projectile_" + name.ToString()));

        if (ui != null)
        {
            return ui;
        }
        return null;
    }

    public T Load<T>(string name) where T : Component
    {
        T[] objects = Resources.LoadAll<T>("Prefabs");

        foreach (T obj in objects)
        {
            if (obj.name == name)
            {
                return Instantiate(obj);
            }
        }

        return null;
    }

    public GameObject Load(FXNames fxName)
    {
        GameObject fx = Instantiate(Resources.Load<GameObject>("Prefabs/FX/FX_" + fxName.ToString()));

        if (fx != null)
        {
            return fx;
        }
        return null;
    }

    public GameObject Load(CharacterNames characterName)
    {
        GameObject monster = Instantiate(Resources.Load<GameObject>("Prefabs/Monster/" + characterName.ToString()));

        if (monster != null)
        {
            return monster;
        }
        return null;
    }

    public GameObject Load(StageNames stageName)
    {
        GameObject stage = Instantiate(Resources.Load<GameObject>("Prefabs/Stage/" + stageName.ToString()));

        if (stage != null)
        {
            return stage;
        }
        return null;
    }

    public T LoadScriptable<T>(string name) where T : ScriptableObject
    {
        T[] objects = Resources.LoadAll<T>("ScriptableObject");

        foreach (T obj in objects)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }

        return null;
    }

    public WeaponData LoadWeaponScriptable(WeaponNames name)
    {
        WeaponData[] objects = Resources.LoadAll<WeaponData>("ScriptableObject/Weapon");

        foreach (WeaponData obj in objects)
        {
            if (obj.Name == name)
            {
                return obj;
            }
        }

        return null;
    }

    public Sprite LoadSprite(string name)
    {
        Sprite[] objects = Resources.LoadAll<Sprite>("Sprite");

        foreach (Sprite obj in objects)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }

        return null;
    }
}