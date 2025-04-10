using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    public Character Owner;

    public Dictionary<BuffNames, Buff> Buffs = new();

    private void OnEnable()
    {
        EventManager<EventTypes>.Register(EventTypes.ChangeMonsterLevel, ChangeMonsterLevel);
        EventManager<EventTypes>.Register(EventTypes.PlayerAttackToNoCritical, AttackToNoCritical);
        EventManager<EventTypes>.Register(EventTypes.PlayerAttackToCritical, AttackToCritical);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister(EventTypes.ChangeMonsterLevel, ChangeMonsterLevel);
        EventManager<EventTypes>.Register(EventTypes.PlayerAttackToNoCritical, AttackToNoCritical);
        EventManager<EventTypes>.Register(EventTypes.PlayerAttackToCritical, AttackToCritical);
    }

    public void Register(BuffNames buffNames, float aliveTime, float value)
    {
        if (false == Buffs.ContainsKey(buffNames))
        {
            LogManager.LogInfo(LogTypes.Buff, string.Format("[{0}] 가 등록됩니다.", buffNames.ToString()));

            Buff buff = ResourcesManager.Instance.Load(buffNames).GetComponent<Buff>();
            buff.transform.SetParent(transform);
            buff.AliveTime = aliveTime;
            buff.Value = value;
            Buffs.Add(buffNames, buff);
        }
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
                    buffs[i].Actiavte(Owner);
                }

                if (buffs[i].EndConditions == BuffConditions.MonsterSpawnd)
                {
                    buffs[i].Deactivate();
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
                    buffs[i].Actiavte(Owner);
                }

                if (buffs[i].EndConditions == BuffConditions.PlayerAttackToNoCritical)
                {
                    buffs[i].Deactivate();
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
                    buffs[i].Actiavte(Owner);
                }

                if (buffs[i].EndConditions == BuffConditions.PlayerAttackToCritical)
                {
                    buffs[i].Deactivate();
                }
            }
        }
    }

    public void Deactivate(BuffNames buffNames)
    {
        Buffs[buffNames].gameObject.SetActive(false);
    }
}