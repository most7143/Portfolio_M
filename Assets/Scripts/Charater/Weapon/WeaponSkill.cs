using UnityEngine;

public class WeaponSkill : MonoBehaviour
{
    public bool Alive;
    public Player Owner;
    public WeaponSkillNames Name;
    public FXNames FXName;
    public string NameString;
    public SkillTypes Type;
    public SkillConditions Condition;
    public float Chance;
    public float ValueMultiply;

    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(NameString))
        {
            Name = EXEnum.Parse<WeaponSkillNames>(NameString);
            FXName = EXEnum.Parse<FXNames>(NameString);
            transform.name = NameString;
        }
    }

    public void ActivateByChance()
    {
        float rand = Random.Range(0, 1f);

        float chance = Chance * Owner.StatSystem.GetStat(StatNames.WeaponTriggerChance);

        if (chance >= rand)
        {
            Activate();
        }
    }

    public void Activate()
    {
        if (Type == SkillTypes.Attack)
        {
            FXManager.Instance.Spawn(FXName, Owner);

            DamageInfo damageInfo = new DamageInfo();

            damageInfo.Owner = Owner;
            damageInfo.Type = DamageTypes.WeaponSkill;
            damageInfo.Value = (long)Owner.Attack;

            InGameManager.Instance.Monster.CalculateHitDamage(ref damageInfo);

            damageInfo.Value = Mathf.RoundToInt(damageInfo.Value * Owner.StatSystem.GetStat(StatNames.WeaponSkillDamageRate) * ValueMultiply);

            damageInfo.IsCritical = Random.Range(0f, 1f) <= Owner.StatSystem.GetStat(StatNames.CriticalChance);

            Owner.SkillAttack(ref damageInfo);
        }
        else if (Type == SkillTypes.Buff)
        {
        }
    }
}