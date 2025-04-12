using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    public BuffNames Name;
    public BuffTypes Type;
    public BuffConditions Conditions;
    public BuffConditions EndConditions;

    public BuffNames IgnoreBuffName;

    public Character Owner;

    public StatNames StatName;

    [HideInInspector] public float Value;

    [HideInInspector] public float AliveTime;

    public float CoolDown;

    public Image Icon;

    private Coroutine _coroutine;

    public void Activate()
    {
        Buff ignoreBuff = Owner.BuffSystem.GetBuff(IgnoreBuffName);

        if (ignoreBuff != null)
        {
            Owner.BuffSystem.Deactivate(Name);
            return;
        }

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

        OnTrigger();

        yield return new WaitForSeconds(AliveTime);

        Deactivate();
    }

    public void OnTrigger()
    {
        if (Name == BuffNames.Enforcer || Name == BuffNames.Enforcer2)
        {
            Owner.StatSystem.IsInvincibility = true;
        }
    }

    public void OffTrigger()
    {
        if (Name == BuffNames.Enforcer || Name == BuffNames.Enforcer2)
        {
            Owner.StatSystem.IsInvincibility = false;
        }
    }
}