using System.Collections.Generic;
using UnityEngine;

public class ClassTraitSystem : MonoBehaviour
{
    public List<ClassTraitNames> Traits = new();

    private void OnEnable()
    {
        EventManager<EventTypes>.Register<int>(EventTypes.LevelUp, OpenPopup);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister<int>(EventTypes.LevelUp, OpenPopup);
    }

    public void Add(ClassTraitNames name)
    {
        Traits.Add(name);
    }

    public void OpenPopup(int level)
    {
        if (level % 2 == 0)
        {
            UIPopupManager.Instance.Spawn(UIPopupNames.ClassTrait);
        }
    }
}