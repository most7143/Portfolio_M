using UnityEngine;

public class WeaponSkill : MonoBehaviour
{
    public bool Alive;
    public Player Owner;
    public WeaponSkillNames Name;
    public FXNames FXName;
    public SkillTypes Type;
    public SkillConditions Condition;
    public float Chance;
    public float ValueMultiply;

    public void Activate()
    {
        float rand = Random.Range(0, 1f);

        if (Chance >= rand)
        {
            if (Type == SkillTypes.Attack)
            {
                FXManager.Instance.Spawn(FXName, Owner);

                DamageInfo damageInfo = new DamageInfo();

                damageInfo.Type = DamageTypes.Skill;
                damageInfo.Value = Mathf.RoundToInt(Owner.Attack * ValueMultiply);

                Owner.SkillAttack(damageInfo);
            }
            else if (Type == SkillTypes.Buff)
            {
            }
        }
    }
}