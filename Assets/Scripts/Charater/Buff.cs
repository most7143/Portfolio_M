using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    public BuffNames Name;
    public BuffTypes Type;
    public BuffConditions Conditions;
    public BuffConditions EndConditions;
    [HideInInspector] public Character Owner;

    public StatNames StatName;

    [HideInInspector] public float Value;

    [HideInInspector] public float AliveTime;

    public Image Icon;

    private Coroutine _coroutine;

    public void Actiavte(Character character)
    {
        Owner = character;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ProcessActivate());
    }

    public void Deactivate()
    {
        if (Owner != null)
        {
            if (Type == BuffTypes.Stat)
            {
                Owner.StatSystem.RemoveStat(StatTID.Buff, StatName);
            }
            else if (Type == BuffTypes.Stack)
            {
                Owner.StatSystem.RemoveStat(StatTID.BuffStack, StatName);
            }
            else if (Type == BuffTypes.Trigger)
            {
                OffTrigger();
            }

            Owner.BuffSystem.Deactivate(Name);
        }
    }

    private IEnumerator ProcessActivate()
    {
        LogManager.LogInfo(LogTypes.Buff, string.Format("[{0}] 효과 발동", Name.ToString()));

        if (Type == BuffTypes.Stat)
        {
            Owner.StatSystem.AddStat(StatTID.Buff, StatName, Value);
        }
        else if (Type == BuffTypes.Stack)
        {
            Owner.StatSystem.AddStat(StatTID.BuffStack, StatName, Value);
        }
        else if (Type == BuffTypes.Trigger)
        {
            OnTrigger();
        }

        yield return new WaitForSeconds(AliveTime);

        Deactivate();
    }

    private void OnTrigger()
    {
        if (Name == BuffNames.Enforcer)
        {
            Owner.StatSystem.IsInvincibility = true;
        }
    }

    private void OffTrigger()
    {
        if (Name == BuffNames.Enforcer)
        {
            Owner.StatSystem.IsInvincibility = false;
        }
    }
}