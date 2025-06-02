using System.Collections.Generic;
using UnityEngine;

public class UICharacterInfoTrait : MonoBehaviour
{
    public List<UITraitSlot> Slots;

    public void Refresh()
    {
        List<ClassTraitNames> names = InGameManager.Instance.Player.ClassTraitSystem.GetNames();

        for (int i = 0; i < names.Count; i++)
        {
            Slots[i].Refresh(names[i], InGameManager.Instance.Player.ClassTraitSystem.ClassTier);
        }
    }
}