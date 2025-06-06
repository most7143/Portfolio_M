using System.Collections.Generic;
using UnityEngine;

public class UICharacterInfoTrait : MonoBehaviour
{
    public List<UITraitSlot> Slots;

    public void Refresh()
    {
        List<ClassTraitNames> names = InGameManager.Instance.Player.ClassTraitSystem.GetNames();

        int count = 0;

        if (InGameManager.Instance.Player.ClassTraitSystem.Name == ClassNames.Swordman)
        {
            count = 0;
        }
        else
        {
            count = 1;
        }

        for (int i = 0; i < names.Count; i++)
        {
            Slots[i].Refresh(names[i], count);
        }
    }
}