using UnityEngine;

public class UIDetailsManager : MonoBehaviour
{
    private static UIDetailsManager instance = null;

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

    public static UIDetailsManager Instance
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

    public UIWeaponDetails WeaponDetails;
}