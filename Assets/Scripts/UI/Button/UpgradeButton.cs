using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public WeaponUpgrade WeaponUpgrade;

    private bool isPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        WeaponUpgrade.Upgrade();

        StartCoroutine(PressProcess());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        StopAllCoroutines();
        CancelInvoke("Execute");
    }

    private IEnumerator PressProcess()
    {
        yield return new WaitForSeconds(0.3f);
        isPressed = true;
        InvokeRepeating("Execute", 0f, 0.1f);
    }

    private void Execute()
    {
        if (isPressed)
        {
            WeaponUpgrade.Upgrade();
        }
    }
}