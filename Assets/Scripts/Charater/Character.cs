using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterNames Name;
    [HideInInspector] public string NameString;
    public int Level = 1;

    public bool IsAlive;

    public SpriteRenderer Renderer;

    public PassiveSystem PassiveSystem;

    public StatSystem StatSystem;

    public BuffSystem BuffSystem;

    public float CurrentHp { get; private set; }

    public float AttackSpeed
    { get { return StatSystem.GetStat(StatNames.AttackSpeed); } }

    public float Attack
    { get { return GetAttackStat(); } }

    public float Armor
    { get { return GetArmorStat(); } }

    public float MaxHP
    {
        get { return GetMaxHealthStat(); }
    }

    protected virtual void Awake()
    {
        IsAlive = true;
        BuffSystem.Owner = this;
    }

    public void StartAttack()
    {
        StartCoroutine(ProcessAttack());
    }

    public virtual void OnAttack()
    {
    }

    public virtual void Hit(ref DamageInfo info)
    {
        if (StatSystem.IsInvincibility)
        {
            return;
        }

        if (Dodge())
        {
            InGameManager.Instance.ObjectPool.SpawnFloaty(transform.position, FloatyTypes.Dodge, "Miss");

            if (Name == CharacterNames.Swordman)
            {
                if (UIManager.Instance.PlayerInfo.UIPassiveSkillInfo.IsMaxSkillLevel(PassiveSkillNames.OmniDirectionalMobility))
                {
                    InGameManager.Instance.Controller.AddGold(InGameManager.Instance.MonsterSpanwer.Gold);
                }
            }

            return;
        }

        StartCoroutine(ProcessHitEffect());

        info.Value = CalculateHitDamage(info.Value);

        if (info.Owner.Name == CharacterNames.Swordman)
        {
            OnHitPlayer(info);
        }

        RefreshHP(-(int)info.Value);

        if (CurrentHp <= 0)
        {
            CurrentHp = 0;

            Dead();
        }
    }

    private void OnHitPlayer(DamageInfo info)
    {
        float onHit = info.Owner.StatSystem.GetStat(StatNames.HealingOnHit);

        if (onHit <= 0)
        {
            return;
        }

        if (0.05f >= Random.Range(0, 1f))
        {
            int healingValue = Mathf.CeilToInt(info.Value * onHit);
            info.Owner.Heal(healingValue);
        }
    }

    public virtual void Dead()
    {
    }

    public IEnumerator ProcessAttack()
    {
        yield return new WaitUntil(() => InGameManager.Instance.IsBattle);
        yield return new WaitForSeconds(1f / AttackSpeed);

        OnAttack();
        StartCoroutine(ProcessAttack());
    }

    public IEnumerator ProcessHitEffect()
    {
        Renderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        Renderer.color = Color.white;
    }

    private bool Dodge()
    {
        float stat = StatSystem.GetStat(StatNames.DodgeRate);

        float rend = Random.Range(0, 1f);

        return stat >= rend;
    }

    public DamageInfo CalculateDamage()
    {
        DamageInfo info = new DamageInfo();

        info.Owner = this;

        info.Value = RandomDamage(Attack);

        float critical = Random.Range(0f, 1f);

        info.IsCritical = critical <= StatSystem.GetStat(StatNames.CriticalChance);

        if (info.IsCritical)
        {
            info.Value *= StatSystem.GetStat(StatNames.CriticalDamage);

            LogManager.LogInfo(LogTypes.Attack, string.Format("[{0}] 피해 (랜덤) = {1} \n 기본 공격력({2}) + 무기 공격력({3}) + 패시브 공격력({4}) + 공격력 배율({5}%) \n" +
  " 치명률({6}%) = 기본({7}%) + 무기({8}%) + 패시브 ({9}%) + 버프({10}%) \n"
  + "치명타 피해({11}%) = 기본({12}%) + 무기({13}%) + 패시브({14}%)",
     name, info.Value, StatSystem.GetStat(StatTID.Base, StatNames.Attack), StatSystem.GetStat(StatTID.Weapon, StatNames.Attack), StatSystem.GetStat(StatTID.PassiveSkill, StatNames.Attack),
     StatSystem.GetStat(StatNames.AttackRate) * 100f, StatSystem.GetStat(StatNames.CriticalChance) * 100f, StatSystem.GetStat(StatTID.Base, StatNames.CriticalChance) * 100f, StatSystem.GetStat(StatTID.Weapon, StatNames.CriticalChance) * 100f, StatSystem.GetStat(StatTID.PassiveSkill, StatNames.CriticalChance) * 100f,
     StatSystem.GetStat(StatTID.Buff, StatNames.CriticalChance) * 100f + StatSystem.GetStat(StatTID.BuffStack, StatNames.CriticalChance) * 100f,
     StatSystem.GetStat(StatNames.CriticalDamage) * 100f, StatSystem.GetStat(StatTID.Base, StatNames.CriticalDamage) * 100f, StatSystem.GetStat(StatTID.Weapon, StatNames.CriticalDamage) * 100f, StatSystem.GetStat(StatTID.PassiveSkill, StatNames.CriticalDamage) * 100f));

            if (Name == CharacterNames.Swordman)
            {
                EventManager<EventTypes>.Send(EventTypes.PlayerAttackToCritical);
            }
        }
        else
        {
            LogManager.LogInfo(LogTypes.Attack, string.Format("[{0}] 피해 (랜덤) = {1} \n 기본 공격력({2}) + 무기 공격력({3}) + 패시브 공격력({4}) + 공격력 배율({5}%) \n",
          name, info.Value, StatSystem.GetStat(StatTID.Base, StatNames.Attack), StatSystem.GetStat(StatTID.Weapon, StatNames.Attack), StatSystem.GetStat(StatTID.PassiveSkill, StatNames.Attack),
          StatSystem.GetStat(StatNames.AttackRate) * 100f));

            if (Name == CharacterNames.Swordman)
            {
                EventManager<EventTypes>.Send(EventTypes.PlayerAttackToNoCritical);
            }
        }

        info.Value = Mathf.RoundToInt(info.Value);

        return info;
    }

    public float CalculateHitDamage(float damage)
    {
        float armor = StatSystem.GetStat(StatNames.Armor);
        float armorRate = StatSystem.GetStat(StatNames.ArmorRate);
        float reduece = StatSystem.GetStat(StatNames.DamageReduction);
        float armorByLevel = StatSystem.GetStat(StatNames.ArmorByLevel);

        float armorValue = armor + (armorByLevel * (Level - 1));

        float result = (damage - armorValue * armorRate);

        float damageMultiplier = Mathf.Clamp(2 - reduece, 0f, 1f);
        result *= damageMultiplier;

        LogManager.LogInfo(LogTypes.Damage,
   name + " /" + " 방어력(" + armorValue + "+" + (armorRate * 100f) + "%) 받는피해감소(" + (reduece - 1f) + "%) = " + result);

        return result > 0 ? result : 1;
    }

    private float RandomDamage(float Damage)
    {
        float min = Damage * 0.9f;
        float max = Damage;

        return Mathf.RoundToInt(Random.Range(min, max));
    }

    private float GetMaxHealthStat()
    {
        float maxHp = (StatSystem.GetStat(StatNames.Health) + (StatSystem.GetStat(StatNames.HealthByLevel) * (Level - 1)))
            * StatSystem.GetStat(StatNames.HealthRate);

        return Mathf.RoundToInt(maxHp);
    }

    private float GetAttackStat()
    {
        float attack = (StatSystem.GetStat(StatNames.Attack) + (StatSystem.GetStat(StatNames.AttackByLevel) * (Level - 1)))
             * StatSystem.GetStat(StatNames.AttackRate);

        return Mathf.RoundToInt(attack);
    }

    private float GetArmorStat()
    {
        float armor = (StatSystem.GetStat(StatNames.Armor) + (StatSystem.GetStat(StatNames.ArmorByLevel) * (Level - 1)))
             * StatSystem.GetStat(StatNames.ArmorRate);

        return Mathf.RoundToInt(armor);
    }

    public void Heal(int value)
    {
        InGameManager.Instance.ObjectPool.SpawnFloaty(transform.position, FloatyTypes.Heal, RefreshHP(value).ToString());
    }

    public float GetCurrentMaxHP()
    {
        int isLimit = (int)(StatSystem.GetStat(StatNames.LimitHealth));

        if (isLimit == 1)
        {
            return MaxHP * 0.7f;
        }
        else
        {
            return MaxHP;
        }
    }

    public float RefreshHP(int value)
    {
        int resultValue = value;

        CurrentHp += value;

        float maxHP = GetCurrentMaxHP();

        if (CurrentHp + value >= maxHP)
        {
            resultValue = (int)((CurrentHp + value) - maxHP);
            CurrentHp = maxHP;
        }
        else
        {
            CurrentHp += resultValue;
        }

        EventManager<EventTypes>.Send(EventTypes.RefreshPlayerHP);

        return resultValue;
    }
}