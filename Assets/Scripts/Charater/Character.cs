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

    public Character Target;

    public float CurrentHp { get; private set; }

    public float AttackSpeed
    { get { return GetAttackSpeed(); } }

    public float Attack
    { get { return GetAttackStat(); } }

    public float Armor
    { get { return GetArmorStat(); } }

    public float MaxHP
    {
        get { return GetMaxHealthStat(); }
    }

    private Coroutine _attackCoroutine;

    protected virtual void Awake()
    {
        IsAlive = true;
        BuffSystem.Owner = this;
    }

    public void StartAttack()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        _attackCoroutine = StartCoroutine(ProcessAttack());
    }

    public virtual void OnAttack()
    {
    }

    public virtual bool Hit(ref DamageInfo info)
    {
        if (StatSystem.GetStat(StatNames.Invincibility) == 1)
        {
            return false;
        }

        if (Dodge())
        {
            InGameManager.Instance.ObjectPool.SpawnFloaty(transform.position, FloatyTypes.Dodge, "Miss");

            if (Name == CharacterNames.Swordman)
            {
                if (info.Type == DamageTypes.Attack || info.Type == DamageTypes.WeaponSkill)
                {
                    if (UIManager.Instance.PlayerInfo.UIPassiveSkillInfo.IsMaxSkillLevel(PassiveSkillNames.OmniDirectionalMobility))
                    {
                        InGameManager.Instance.Controller.AddCurrency(CurrencyTypes.Gold, InGameManager.Instance.MonsterSpanwer.Gold);
                    }
                }
            }

            return false;
        }

        StartCoroutine(ProcessHitEffect());

        CalculateHitDamage(ref info);

        if (info.Owner.Name == CharacterNames.Swordman)
        {
            OnHitPlayer(info);

            float rand = Random.Range(0, 1f);

            if (rand <= info.Owner.StatSystem.GetStat(StatNames.AttackToAddGoldChance))
            {
                InGameManager.Instance.Controller.AddCurrency(CurrencyTypes.Gold, InGameManager.Instance.MonsterSpanwer.Gold);
            }
        }

        Reflection(info);

        RefreshHP(-(int)info.Value);

        if (CurrentHp <= 0)
        {
            CurrentHp = 0;

            Dead();
        }

        SpawnFloaty(info);

        return true;
    }

    private void Reflection(DamageInfo info)
    {
        float DamageReflection = StatSystem.GetStat(StatNames.DamageReflection);

        if (DamageReflection > 0)
        {
            float reflectionValue = info.Value * DamageReflection;
            DamageInfo reflection = new DamageInfo();

            reflection.Value = reflectionValue;
            reflection.Type = DamageTypes.Attack;
            reflection.Owner = this;

            info.Owner.Hit(ref reflection);
        }
    }

    private void SpawnFloaty(DamageInfo info)
    {
        if (info.Owner.Name != CharacterNames.Swordman)
            return;

        if (info.Type == DamageTypes.Attack)
        {
            if (info.IsCritical)
            {
                InGameManager.Instance.ObjectPool.SpawnFloaty(transform.position, FloatyTypes.CritialDamage, info.Value.ToString());
            }
            else
            {
                InGameManager.Instance.ObjectPool.SpawnFloaty(transform.position, FloatyTypes.Damage, info.Value.ToString());
            }
        }
        else if (info.Type == DamageTypes.WeaponSkill)
        {
            Vector3 position = new Vector3(Target.transform.position.x + Random.Range(0, 0.5f), Target.transform.position.y + Random.Range(0, 0.5f));

            if (info.IsCritical)
            {
                InGameManager.Instance.ObjectPool.SpawnFloaty(transform.position, FloatyTypes.CritialDamage, info.Value.ToString());
                InGameManager.Instance.ObjectPool.SpawnFloaty(position, FloatyTypes.CritialSkillDamage, info.Value.ToString());
            }
            else
            {
                InGameManager.Instance.ObjectPool.SpawnFloaty(position, FloatyTypes.SkillDamage, info.Value.ToString());
            }
        }
    }

    private void OnHitPlayer(DamageInfo info)
    {
        float onHit = info.Owner.StatSystem.GetStat(StatNames.HealingOnHit);

        if (onHit <= 0)
        {
            return;
        }

        float percent = 0.05f * (info.Owner.StatSystem.GetStat(StatNames.IncreaseHealingOnHitChance) == 0 ? 1 : info.Owner.StatSystem.GetStat(StatNames.IncreaseHealingOnHitChance));

        Debug.Log(percent);
        if (percent >= Random.Range(0, 1f))
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

        float timer = 0f;

        while (InGameManager.Instance.IsBattle)
        {
            timer += Time.deltaTime * InGameManager.Instance.GameSpeed;

            float interval = 1f / AttackSpeed;

            if (timer >= interval)
            {
                timer -= interval;
                OnAttack();
            }

            yield return null;
        }
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
        info.Type = DamageTypes.Attack;

        info.Value = RandomDamage(Attack);

#if UNITY_EDITOR
        LogManager.LogInfo(LogTypes.Attack, string.Format("[{0}] 피해 (랜덤) = {1} \n(기본 공격력({2}) + 무기 공격력({3}) + 패시브 공격력({4}) + 공격력 배율({5}%) ) * 피해량 배율({6}%) \n",
      name, info.Value, StatSystem.GetStat(StatTID.Base, StatNames.Attack), StatSystem.GetStat(StatTID.Weapon, StatNames.Attack), StatSystem.GetStat(StatTID.PassiveSkill, StatNames.Attack),
      StatSystem.GetStat(StatNames.AttackRate) * 100f, StatSystem.GetStat(StatNames.DamageRate) * 100f));
#endif
        if (Name == CharacterNames.Swordman)
        {
            EventManager<EventTypes>.Send(EventTypes.PlayerAttackToNoCritical);
        }

        info.Value = Mathf.RoundToInt(info.Value * StatSystem.GetStat(StatNames.DamageRate) * 1f);

        return info;
    }

    public void CalculateHitDamage(ref DamageInfo info)
    {
        float armor = StatSystem.GetStat(StatNames.Armor);
        float armorRate = StatSystem.GetStat(StatNames.ArmorRate);
        float reduece = StatSystem.GetStat(StatNames.DamageReduction);
        float armorByLevel = StatSystem.GetStat(StatNames.ArmorByLevel);

        float armorValue = (armor + (armorByLevel * (Level - 1))) * armorRate;

        int IgnoreValue = Mathf.RoundToInt(armorValue * (1 - info.Owner.StatSystem.GetStat(StatNames.IgnoreArmor)));

        float beforeValue = info.Value;

        info.Value = info.Value - IgnoreValue;

        float damageMultiplier = Mathf.Clamp(2 - reduece, 0f, 1f);
        info.Value *= damageMultiplier;

        float critical = Random.Range(0f, 1f);

        info.IsCritical = critical <= info.Owner.StatSystem.GetStat(StatNames.CriticalChance);

        if (info.IsCritical)
        {
            info.Value *= info.Owner.StatSystem.GetStat(StatNames.CriticalDamage);

#if UNITY_EDITOR
            LogManager.LogInfo(LogTypes.Attack, string.Format("공격자 :{0} , 피해량 = {1} \n  치명률({2}%) = 기본({3}%) + 무기({4}%) + 패시브 ({5}%) + 버프({6}%) \n"
  + "치명타 피해({7}%) = 기본({8}%) + 무기({9}%) + 패시브({10}%)",
     Name, info.Value,
     info.Owner.StatSystem.GetStat(StatNames.CriticalChance) * 100f, info.Owner.StatSystem.GetStat(StatTID.Base, StatNames.CriticalChance) * 100f, info.Owner.StatSystem.GetStat(StatTID.Weapon, StatNames.CriticalChance) * 100f, info.Owner.StatSystem.GetStat(StatTID.PassiveSkill, StatNames.CriticalChance) * 100f,
     info.Owner.StatSystem.GetStat(StatTID.Buff, StatNames.CriticalChance) * 100f + info.Owner.StatSystem.GetStat(StatTID.BuffStack, StatNames.CriticalChance) * 100f,
     info.Owner.StatSystem.GetStat(StatNames.CriticalDamage) * 100f, info.Owner.StatSystem.GetStat(StatTID.Base, StatNames.CriticalDamage) * 100f, info.Owner.StatSystem.GetStat(StatTID.Weapon, StatNames.CriticalDamage) * 100f, info.Owner.StatSystem.GetStat(StatTID.PassiveSkill, StatNames.CriticalDamage) * 100f));
#endif
            if (Name == CharacterNames.Swordman)
            {
                EventManager<EventTypes>.Send(EventTypes.PlayerAttackToCritical);
            }
        }

        info.Value = Mathf.RoundToInt(info.Value);

        info.Value = info.Value > 0 ? info.Value : 1;

#if UNITY_EDITOR

        LogManager.LogInfo(LogTypes.Attack, string.Format("{0}가 받는 피해량,  공격 피해량({1}) - 방어력({2},{3}%) * 받는피해감소({4}%) = {5}",
            Name.ToString(), beforeValue, armor, armorRate * 100f, damageMultiplier * 100f, info.Value));

#endif
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
            * (StatSystem.GetStat(StatNames.HealthRate) + StatSystem.GetStat(StatNames.AllStats));

        return Mathf.RoundToInt(maxHp);
    }

    private float GetAttackStat()
    {
        float armorToAttack = (int)(GetArmorStat() * StatSystem.GetStat(StatNames.ArmorConvertToAttack));
        float ciriticalDamageToAttackRate = StatSystem.GetStat(StatNames.CriticalDamage) * StatSystem.GetStat(StatNames.CriticalDamageConvertToAttackRate);

        float attack = (StatSystem.GetStat(StatNames.Attack) + (StatSystem.GetStat(StatNames.AttackByLevel) * (Level - 1)) + armorToAttack)
             * (StatSystem.GetStat(StatNames.AttackRate) + StatSystem.GetStat(StatNames.AllStats) + ciriticalDamageToAttackRate);

        return Mathf.RoundToInt(attack);
    }

    private float GetArmorStat()
    {
        float armor = (StatSystem.GetStat(StatNames.Armor) + (StatSystem.GetStat(StatNames.ArmorByLevel) * (Level - 1)))
             * (StatSystem.GetStat(StatNames.ArmorRate) + StatSystem.GetStat(StatNames.AllStats));

        return Mathf.RoundToInt(armor);
    }

    public float GetAttackSpeed()
    {
        float attackSpeed = StatSystem.GetStat(StatNames.AttackSpeed) * (StatSystem.GetStat(StatNames.DoubleAttackSpeed) == 0 ? 1 : 2);

        return attackSpeed;
    }

    public void Heal(int value)
    {
        InGameManager.Instance.ObjectPool.SpawnFloaty(transform.position, FloatyTypes.Heal, RefreshHP(value).ToString());

        EventManager<EventTypes>.Send(EventTypes.UsingHeal);
    }

    public void HealRate(float valueRate)
    {
        int value = (int)(MaxHP * valueRate);

        InGameManager.Instance.ObjectPool.SpawnFloaty(transform.position, FloatyTypes.Heal, RefreshHP(value).ToString());

        EventManager<EventTypes>.Send(EventTypes.UsingHeal);
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