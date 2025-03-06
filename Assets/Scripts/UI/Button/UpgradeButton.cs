using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public WeaponUpgrade WeaponUpgrade;

    private bool isPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        InvokeRepeating(nameof(ExecuteWhileHolding), 0f, 0.1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        CancelInvoke(nameof(ExecuteWhileHolding)); // 중지
    }

    private void ExecuteWhileHolding()
    {
        if (isPressed)
        {
            WeaponUpgrade.Upgrade();
        }
    }
}