using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class XButton : Button
{
    public Action OnExecute;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        Execute();
        StartCoroutine(PressProcess());
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        StopAllCoroutines();
        CancelInvoke("Execute");
    }

    private IEnumerator PressProcess()
    {
        yield return new WaitForSeconds(0.3f);
        InvokeRepeating("Execute", 0f, 0.1f);
    }

    public void Execute()
    {
        if (OnExecute != null)
        {
            OnExecute.Invoke();
        }
    }
}