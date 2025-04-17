using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    public Character Owner;

    public Dictionary<BuffNames, Buff> Buffs = new();
    public Dictionary<BuffNames, Coroutine> CooldownSkills = new();

    private void OnEnable()
    {
        EventManager<EventTypes>.Register(EventTypes.ChangeMonsterLevel, ChangeMonsterLevel);
        EventManager<EventTypes>.Register(EventTypes.PlayerAttackToNoCritical, AttackToNoCritical);
        EventManager<EventTypes>.Register(EventTypes.PlayerAttackToCritical, AttackToCritical);
        EventManager<EventTypes>.Register(EventTypes.PlayerDamaged, PlayerDamaged);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister(EventTypes.ChangeMonsterLevel, ChangeMonsterLevel);
        EventManager<EventTypes>.Unregister(EventTypes.PlayerAttackToNoCritical, AttackToNoCritical);
        EventManager<EventTypes>.Unregister(EventTypes.PlayerAttackToCritical, AttackToCritical);
        EventManager<EventTypes>.Unregister(EventTypes.PlayerDamaged, PlayerDamaged);
    }

    public void Register(BuffNames buffNames, float aliveTime, float value)
    {
        if (false == Buffs.ContainsKey(buffNames))
        {
#if UNITY_EDITOR
            LogManager.LogInfo(LogTypes.Buff, string.Format("[{0}] 가 등록됩니다.", buffNames.ToString()));
#endif

            Buff buff = ResourcesManager.Instance.Load(buffNames).GetComponent<Buff>();
            buff.transform.SetParent(transform);
            buff.Owner = Owner;
            buff.AliveTime = aliveTime;
            buff.Value = value;
            Buffs.Add(buffNames, buff);

            if (buff.Conditions == BuffConditions.CoolDown)
            {
                RegisterCoolDownSkills(buffNames);
            }
            else if (buff.Conditions == BuffConditions.None)
            {
                buff.Activate();
            }
        }
    }

    public Buff GetBuff(BuffNames name)
    {
        if (Buffs.ContainsKey(name))
        {
            if (Buffs[name].gameObject.activeSelf)
            {
                return Buffs[name];
            }
        }
        return null;
    }

    public void ChangeMonsterLevel()
    {
        Buff[] buffs = Buffs.Values.ToArray();

        if (buffs.Length > 0)
        {
            for (int i = 0; i < buffs.Length; i++)
            {
                if (buffs[i].Conditions == BuffConditions.MonsterSpawnd)
                {
                    buffs[i].gameObject.SetActive(true);
                    buffs[i].Activate();
                }

                if (buffs[i].EndConditions == BuffConditions.MonsterSpawnd)
                {
                    Deactivate(buffs[i].Name);
                }
            }
        }
    }

    public void AttackToNoCritical()
    {
        Buff[] buffs = Buffs.Values.ToArray();

        if (buffs.Length > 0)
        {
            for (int i = 0; i < buffs.Length; i++)
            {
                if (false == Buffs.ContainsKey(buffs[i].Name))
                    continue;

                if (buffs[i].Conditions == BuffConditions.PlayerAttackToNoCritical)
                {
                    buffs[i].gameObject.SetActive(true);
                    buffs[i].Activate();
                }

                if (buffs[i].EndConditions == BuffConditions.PlayerAttackToNoCritical)
                {
                    Deactivate(buffs[i].Name);
                }
            }
        }
    }

    public void AttackToCritical()
    {
        Buff[] buffs = Buffs.Values.ToArray();

        if (buffs.Length > 0)
        {
            for (int i = 0; i < buffs.Length; i++)
            {
                if (buffs[i].Conditions == BuffConditions.PlayerAttackToCritical)
                {
                    buffs[i].gameObject.SetActive(true);
                    buffs[i].Activate();
                }

                if (buffs[i].EndConditions == BuffConditions.PlayerAttackToCritical)
                {
                    Deactivate(buffs[i].Name);
                }
            }
        }
    }

    public void PlayerDamaged()
    {
        Buff[] buffs = Buffs.Values.ToArray();

        if (buffs.Length > 0)
        {
            for (int i = 0; i < buffs.Length; i++)
            {
                if (buffs[i].Conditions == BuffConditions.PlayerDamaged)
                {
                    buffs[i].gameObject.SetActive(true);
                    buffs[i].Activate();
                }

                if (buffs[i].EndConditions == BuffConditions.PlayerDamaged)
                {
                    Deactivate(buffs[i].Name);
                }
            }
        }
    }

    public void Deactivate(BuffNames buffNames)
    {
        if (Buffs[buffNames].Type == BuffTypes.Stat)
        {
            Owner.StatSystem.RemoveStat(StatTID.Buff, Buffs[buffNames].StatName);
        }
        else if (Buffs[buffNames].Type == BuffTypes.Stack)
        {
            Owner.StatSystem.RemoveStat(StatTID.BuffStack, Buffs[buffNames].StatName);
        }

        Buffs[buffNames].OffTrigger();

        Buffs[buffNames].gameObject.SetActive(false);
    }

    public void RegisterCoolDownSkills(BuffNames buffNames)
    {
        if (Buffs.ContainsKey(buffNames))
        {
            Coroutine coroutine = StartCoroutine(ProcessCoolodwn(buffNames, Buffs[buffNames].CoolDown));

            CooldownSkills.Add(buffNames, coroutine);
        }
    }

    private IEnumerator ProcessCoolodwn(BuffNames buffNames, float Cooldown)
    {
        while (true)
        {
            Buffs[buffNames].gameObject.SetActive(true);
            Buffs[buffNames].Activate();
            yield return new WaitForSeconds(Cooldown);
        }
    }
}