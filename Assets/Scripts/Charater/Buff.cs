using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    public Character Owner;
    public BuffNames Name;
    public string NameString;
    public BuffTypes Type;
    public int MaxStack = 1;
    public BuffConditions Conditions;
    public float ConditionValue;
    public BuffConditions EndConditions;

    public BuffNames IgnoreBuffName;
    public string IgnoreNameString;
    public StatNames StatName;
    public string StatString;
    public ProjectileNames ProjectileName;
    public string ProjectileNameString;
    public Animator Anim;

    public float CoolDown;
    public BuffNames CooldownToBuff;
    public string CooldownToBuffNameString;

    [Header("Boolean")] public bool IsCooldown;
    public bool IgnoreRegisterActivate;

    [HideInInspector] public float Value;

    [HideInInspector] public float AliveTime;

    public Image Icon;

    private Coroutine _coroutine;
    private int _currentStack;

    private int _currentConditionsValue = 0;

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

        if (!string.IsNullOrEmpty(CooldownToBuffNameString))
        {
            CooldownToBuff = EXEnum.Parse<BuffNames>(CooldownToBuffNameString);
        }
    }

    public void Activate()
    {
        _currentConditionsValue = 0;

        if (CoolDown > 0)
        {
            Owner.BuffSystem.RegisterCoolDownSkills(Name);
        }

        if (Type == BuffTypes.Stack)
        {
            _currentStack++;
        }

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ProcessActivate());
    }

    public bool TryConditionValue()
    {
        if (ConditionValue <= 1)
            return true;

        _currentConditionsValue++;

        return _currentConditionsValue >= ConditionValue;
    }

    public bool TryMaxStack()
    {
        return _currentStack >= MaxStack;
    }

    public void Deactivate()
    {
        _currentStack = 0;

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

            OffTrigger();
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
        else if (Type == BuffTypes.Heal)
        {
            Owner.Heal(Mathf.RoundToInt(Owner.MaxHP * Value));
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
            Owner.BuffSystem.Deactivate(Name);
        }
    }

    public void OnTrigger()
    {
        if (Name == BuffNames.Enforcer || Name == BuffNames.Enforcer2)
        {
            Owner.StatSystem.AddStat(StatTID.PassiveSkill, StatNames.Invincibility, 1);
        }
    }

    public void OffTrigger()
    {
        if (Name == BuffNames.Enforcer || Name == BuffNames.Enforcer2)
        {
            Owner.StatSystem.RemoveStat(StatTID.PassiveSkill, StatNames.Invincibility);
        }
    }
}