using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public XButton Button;

    private void Start()
    {
        Button.OnExecute = Click;
        Button.IsPressClick = false;
    }

    public void Click()
    {
        UIPopupManager.Instance.Despawn();
    }
}