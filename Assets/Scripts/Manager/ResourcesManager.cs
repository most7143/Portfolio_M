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
}