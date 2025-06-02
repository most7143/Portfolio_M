using System.Collections.Generic;
using UnityEngine;

public class UIClassTraitPopup : UIPopup
{
    public List<ClassTraitCard> Cards;

    public UICharacterInfoTrait Traits;

    public XButton RerollButton;

    private int rerollCount = 0;

    private void Start()
    {
        RerollButton.OnExecute = Reroll;
        RerollButton.IsPressClick = false;
    }

    public override void Spawn()
    {
        base.Spawn();

        rerollCount = (int)InGameManager.Instance.Player.StatSystem.GetStat(StatNames.AddRerollTrait);

        if (rerollCount > 0)
        {
            RerollButton.gameObject.SetActive(true);
        }
        else
        {
            RerollButton.gameObject.SetActive(false);
        }

        Traits.Refresh();

        Activate();
    }

    private void Activate()
    {
        List<ClassTraitNames> names = InGameManager.Instance.Player.ClassTraitSystem.GetNames();

        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(1, names.Count);

            Cards[i].Refresh(names[rand]);

            names.RemoveAt(rand);
        }
    }

    private void Reroll()
    {
        rerollCount--;
        Activate();

        if (rerollCount == 0)
        {
            RerollButton.gameObject.SetActive(false);
        }
    }
}