using UnityEngine;
using UnityEngine.UI;

public class UIPocketBox : MonoBehaviour
{
    public PocketTypes Type;
    public Image Icon;

    private PocketData data;

    public XButton Button;

    private void Start()
    {
        Button.OnExecute = Click;
    }

    public void Actiavte()
    {
        if (data == null)
        {
            data = ResourcesManager.Instance.LoadScriptable<PocketData>(Type.ToString() + "Pocket");
        }
    }

    public void Click()
    {
    }
}