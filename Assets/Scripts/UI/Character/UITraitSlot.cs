using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITraitSlot : MonoBehaviour
{
    public ClassTraitNames Name;
    public bool IsSet = false;
    public Image Icon;
    public TextMeshProUGUI NameText;

    public void Refresh(ClassTraitNames name)
    {
        ClassTraitData data = ResourcesManager.Instance.LoadScriptable<ClassTraitData>(name.ToString());

        Name = name;
        IsSet = true;
        NameText.SetText(data.NameString);
    }
}