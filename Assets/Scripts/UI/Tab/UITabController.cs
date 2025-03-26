using System.Collections.Generic;
using UnityEngine;

public class UITabController : MonoBehaviour
{
    public List<UITab> Tabs;

    public int SelectedIndex;

    private void Start()
    {
        Tabs[0].Activate();
    }

    public void Select(int index)
    {
        if (index == SelectedIndex)
            return;

        for (int i = 0; i < Tabs.Count; i++)
        {
            if (Tabs[i].Index == index)
            {
                Tabs[i].Activate();
            }
            else
            {
                Tabs[i].Deactivate();
            }
        }
    }
}