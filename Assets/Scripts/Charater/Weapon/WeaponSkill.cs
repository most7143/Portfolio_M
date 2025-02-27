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
    public float Value;

    public void Activate()
    {
        float rand = Random.Range(0, 1f);

        if (Chance >= rand)
        {
            if (Type == SkillTypes.Attack)
            {
                FXManager.Instance.Spawn(FXName, Owner.TargetMonster.transform.position);
            }
            else if (Type == SkillTypes.Buff)
            {
            }
        }
    }
}