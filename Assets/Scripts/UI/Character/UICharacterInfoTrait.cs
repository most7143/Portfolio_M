using System.Collections.Generic;
using UnityEngine;

public class UICharacterInfoTrait : MonoBehaviour
{
    public List<UITraitSlot> Slots;

    private void Start()
    {
        List<ClassTraitNames> names = InGameManager.Instance.Player.ClassTraitSystem.GetNames();

        for (int i = 0; i < names.Count; i++)
        {
            if (false == Slots[i].IsSet)
            {
                Slots[i].Init(names[i]);
            }
        }
    }

    private void OnEnable()
    {
        int tier = InGameManager.Instance.Player.ClassTraitSystem.ClassTier;

        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].IsSet)
            {
                Slots[i].Refresh(tier);
            }
        }
    }

    private void OnDisable()
    {
    }
}