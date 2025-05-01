using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class XButton : Button
{
    public bool IsPressClick = true;
    public Action OnExecute;

    private Coroutine pressRoutine;

    protected override void OnDisable()
    {
        if (pressRoutine != null)
        {
            StopCoroutine(pressRoutine);
            pressRoutine = null;
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        if (interactable)
        {
            Execute();

            if (IsPressClick)
            {
                if (pressRoutine != null)
                    StopCoroutine(pressRoutine);

                if (gameObject.activeInHierarchy)
                {
                    pressRoutine = StartCoroutine(PressProcess());
                }
            }
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        if (IsPressClick)
        {
            StopAllCoroutines();
            CancelInvoke("Execute");
        }
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

    public void RefreshInteraction(bool isAlive)
    {
        interactable = isAlive;

        if (image != null)
        {
            if (interactable)
            {
                image.color = Color.white;
            }
            else
            {
                image.color = Color.gray;
            }
        }
    }
}