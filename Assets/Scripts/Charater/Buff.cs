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
    public StatNames StatName2;
    public string StatString2;
    public ProjectileNames ProjectileName;
    public string ProjectileNameString;
    public Animator Anim;

    public float CoolDown;
    public BuffNames CooldownToBuff;
    public string CooldownToBuffNameString;

    public BuffNames DeactivateToBuff;
    public string DeactivateToBuffNameString;

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

        if (!string.IsNullOrEmpty(StatString2))
        {
            StatName2 = EXEnum.Parse<StatNames>(StatString2);
        }

        if (!string.IsNullOrEmpty(ProjectileNameString))
        {
            ProjectileName = EXEnum.Parse<ProjectileNames>(ProjectileNameString);
        }

        if (!string.IsNullOrEmpty(CooldownToBuffNameString))
        {
            CooldownToBuff = EXEnum.Parse<BuffNames>(CooldownToBuffNameString);
        }

        if (!string.IsNullOrEmpty(DeactivateToBuffNameString))
        {
            DeactivateToBuff = EXEnum.Parse<BuffNames>(DeactivateToBuffNameString);
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
                if (StatName != StatNames.None)
                {
                    Owner.StatSystem.RemoveStat(StatTID.Buff, StatName);
                }

                if (StatName2 != StatNames.None)
                {
                    Owner.StatSystem.RemoveStat(StatTID.Buff, StatName2);
                }
            }
            else if (Type == BuffTypes.Stack)
            {
                if (StatName != StatNames.None)
                {
                    Owner.StatSystem.RemoveStat(StatTID.BuffStack, StatName);
                }

                if (StatName2 != StatNames.None)
                {
                    Owner.StatSystem.RemoveStat(StatTID.BuffStack, StatName2);
                }
            }

            OffTrigger();

            if (DeactivateToBuff != BuffNames.None)
            {
                Owner.BuffSystem.Activate(DeactivateToBuff);
            }
        }
    }

    private IEnumerator ProcessActivate()
    {
#if UNITY_EDITOR
        LogManager.LogInfo(LogTypes.Buff, string.Format("[{0}] 효과 발동", Name.ToString()));
#endif

        if (Type == BuffTypes.Stat)
        {
            if (StatName != StatNames.None)
            {
                Owner.StatSystem.AddStat(StatTID.Buff, StatName, Value);
            }

            if (StatName2 != StatNames.None)
            {
                Owner.StatSystem.AddStat(StatTID.Buff, StatName2, Value);
            }
        }
        else if (Type == BuffTypes.Stack)
        {
            if (StatName != StatNames.None)
            {
                Owner.StatSystem.AddStat(StatTID.BuffStack, StatName, Value);
            }

            if (StatName2 != StatNames.None)
            {
                Owner.StatSystem.AddStat(StatTID.BuffStack, StatName2, Value);
            }
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