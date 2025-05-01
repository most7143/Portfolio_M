using System.Collections.Generic;
using UnityEngine;

public class UIClassTraitPopup : UIPopup
{
    public List<ClassTraitCard> Cards;

    public override void Spawn()
    {
        base.Spawn();

        Activate();
    }

    private void Activate()
    {
        ClassTraitNames[] traitNames = (ClassTraitNames[])System.Enum.GetValues(typeof(ClassTraitNames));

        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(1, traitNames.Length);

            Cards[i].Refresh(traitNames[rand]);
        }
    }
}