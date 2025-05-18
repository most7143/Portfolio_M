using System.Collections;
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
        EventManager<EventTypes>.Register(EventTypes.PlayerDamaged, PlayerDamaged);
    }

    private void OnDisable()
    {
        EventManager<EventTypes>.Unregister(EventTypes.ChangeMonsterLevel, ChangeMonsterLevel);
        EventManager<EventTypes>.Unregister(EventTypes.PlayerAttackToNoCritical, AttackToNoCritical);
        EventManager<EventTypes>.Unregister(EventTypes.PlayerAttackToCritical, AttackToCritical);
        EventManager<EventTypes>.Unregister(EventTypes.PlayerDamaged, PlayerDamaged);
    }

    public void Register(BuffNames buffNames, float aliveTime = 0, float value = 0)
    {
        if (false == Buffs.ContainsKey(buffNames))
        {
#if UNITY_EDITOR
            LogManager.LogInfo(LogTypes.Buff, string.Format("{0} , [{1}] 가 등록됩니다.", Owner.Name, buffNames.ToString()));
#endif

            Buff buff = ResourcesManager.Instance.Load(buffNames).GetComponent<Buff>();
            buff.transform.SetParent(transform);
            buff.transform.localPosition = Vector2.zero;
            buff.Owner = Owner;
            buff.AliveTime = aliveTime;
            buff.Value = value;
            Buffs.Add(buffNames, buff);

            if (buff.IgnoreRegisterActivate)
            {
                Deactivate(buffNames);
            }
            else
            {
                Activate(buffNames);
            }
        }
        else
        {
            Activate(buffNames);
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
                if (buffs[i].EndConditions == BuffConditions.MonsterSpawnd)
                {
                    Deactivate(buffs[i].Name);
                }

                if (buffs[i].Conditions == BuffConditions.MonsterSpawnd)
                {
                    Activate(buffs[i].Name);
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

                if (buffs[i].EndConditions == BuffConditions.PlayerAttackToNoCritical)
                {
                    Deactivate(buffs[i].Name);
                }

                if (buffs[i].Conditions == BuffConditions.PlayerAttackToNoCritical)
                {
                    Activate(buffs[i].Name);
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
                if (buffs[i].EndConditions == BuffConditions.PlayerAttackToCritical)
                {
                    Deactivate(buffs[i].Name);
                }

                if (buffs[i].Conditions == BuffConditions.PlayerAttackToCritical)
                {
                    Activate(buffs[i].Name);
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
                if (buffs[i].EndConditions == BuffConditions.PlayerDamaged)
                {
                    Deactivate(buffs[i].Name);
                }

                if (buffs[i].Conditions == BuffConditions.PlayerDamaged)
                {
                    Activate(buffs[i].Name);
                }
                else if (buffs[i].Conditions == BuffConditions.DecreaseHealth)
                {
                    if (Owner.CurrentHp / Owner.MaxHP <= buffs[i].ConditionValue)
                    {
                        Activate(buffs[i].Name);
                    }
                }
            }
        }
    }

    public void Activate(BuffNames buffName)
    {
        if (Buffs[buffName].IsCooldown)
            return;

        if (Buffs[buffName].IgnoreBuffName != BuffNames.None)
        {
            Buff ignoreBuff = GetBuff(buffName);

            if (ignoreBuff != null)
                return;
        }

        if (Buffs[buffName].TryMaxStack())
        {
            return;
        }

        if (false == Buffs[buffName].TryConditionValue())
        {
            return;
        }

        Buffs[buffName].gameObject.SetActive(true);

        Buffs[buffName].Activate();
    }

    public void Deactivate(BuffNames buffName)
    {
        Buffs[buffName].gameObject.SetActive(false);
        Buffs[buffName].Deactivate();
    }

    public void RegisterCoolDownSkills(BuffNames buffNames)
    {
        if (Buffs.ContainsKey(buffNames))
        {
            Coroutine coroutine = StartCoroutine(ProcessCoolodwn(buffNames, Buffs[buffNames].CoolDown));
        }
    }

    private IEnumerator ProcessCoolodwn(BuffNames buffNames, float Cooldown)
    {
        Buffs[buffNames].IsCooldown = true;
        yield return new WaitForSeconds(Cooldown);
        Buffs[buffNames].IsCooldown = false;

        if (Buffs[buffNames].CooldownToBuff != BuffNames.None)
        {
            Activate(buffNames);
        }
    }
}