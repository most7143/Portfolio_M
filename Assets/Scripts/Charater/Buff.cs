using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    public Character Owner;
    public BuffNames Name;
    public string NameString;
    public BuffTypes Type;
    public BuffConditions Conditions;
    public BuffConditions EndConditions;
    public BuffNames IgnoreBuffName;
    public string IgnoreNameString;
    public StatNames StatName;
    public string StatString;
    public ProjectileNames ProjectileName;
    public string ProjectileNameString;

    [HideInInspector] public float Value;

    [HideInInspector] public float AliveTime;

    public float CoolDown;

    public Image Icon;

    private Coroutine _coroutine;

    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(NameString))
        {
            Name = EXEnum.Parse<BuffNames>(NameString);
        }

        if (!string.IsNullOrEmpty(IgnoreNameString))
        {
            IgnoreBuffName = EXEnum.Parse<BuffNames>(IgnoreNameString);
        }

        if (!string.IsNullOrEmpty(StatString))
        {
            StatName = EXEnum.Parse<StatNames>(StatString);
        }

        if (!string.IsNullOrEmpty(ProjectileNameString))
        {
            ProjectileName = EXEnum.Parse<ProjectileNames>(ProjectileNameString);
        }
    }

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
#if UNITY_EDITOR
        LogManager.LogInfo(LogTypes.Buff, string.Format("[{0}] 효과 발동", Name.ToString()));
#endif

        if (Type == BuffTypes.Stat)
        {
            Owner.StatSystem.AddStat(StatTID.Buff, StatName, Value);
        }
        else if (Type == BuffTypes.Stack)
        {
            Owner.StatSystem.AddStat(StatTID.BuffStack, StatName, Value);
        }

        if (ProjectileName != ProjectileNames.None)
        {
            Player player = Owner as Player;

            if (player != null)
            {
                player.ProjectileSystem.Register(ProjectileName);
                player.ProjectileSystem.Shot(ProjectileName);
            }
        }

        OnTrigger();

        if (AliveTime > 0)
        {
            yield return new WaitForSeconds(AliveTime);
            Deactivate();
        }
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