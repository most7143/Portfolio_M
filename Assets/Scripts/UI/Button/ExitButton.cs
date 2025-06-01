using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public XButton Button;

    private void Start()
    {
        Button.onClick.AddListener(() => Click());
    }

    public void Click()
    {
        UIPopupManager.Instance.Despawn();
    }
}