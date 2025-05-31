using System;
using System.Collections.Generic;
using System.Linq;

public class UIChallengePopup : UIPopup
{
    public List<UIChallengeBox> Boxes;

    public override void Spawn()
    {
        base.Spawn();
        UIChallengeNames[] names = Enum.GetValues(typeof(UIChallengeNames)).Cast<UIChallengeNames>().ToArray();
        for (int i = 0; i < Boxes.Count; i++)
        {
            Boxes[i].Refresh(OutGameManager.Instance.GetChallengeData(names[i + 1]));
        }
    }

    public override void Despawn()
    {
        base.Despawn();
    }
}