using System.Collections.Generic;
using UnityEngine;

public class UICharacterInfoTrait : MonoBehaviour
{
    public List<UITraitSlot> Slots;

    private void OnEnable()
    {
        EventManager<EventTypes>.Register<ClassTraitNames>(EventTypes.AddTrait, AddTrait);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister<ClassTraitNames>(EventTypes.AddTrait, AddTrait);
    }

    private void AddTrait(ClassTraitNames name)
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if (false == Slots[i].IsSet)
            {
                Slots[i].Refresh(name);
                break;
            }
        }
    }
}