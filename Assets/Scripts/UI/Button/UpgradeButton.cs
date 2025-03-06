using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public WeaponUpgrade WeaponUpgrade;

    private bool isPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        InvokeRepeating("Execute", 0f, 0.1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        CancelInvoke("Execute");
    }

    private void Execute()
    {
        if (isPressed)
        {
            WeaponUpgrade.Upgrade();
        }
    }
}