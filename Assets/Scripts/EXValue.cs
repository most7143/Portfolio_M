using UnityEngine;

public static class EXValue
{
    public static GradeNames GetChanceByGrade()
    {
        float rand = Random.Range(0, 1f);
        GradeNames[] gradeNames = (GradeNames[])System.Enum.GetValues(typeof(GradeNames));

        float chance = 0;
        for (int i = 1; i < gradeNames.Length; i++)
        {
            chance += GradeByChance(gradeNames[i]);

            if (rand <= chance)
            {
                return gradeNames[i];
            }
        }

        return GradeNames.Normal;
    }

    private static float GradeByChance(GradeNames grade)
    {
        float addChance = 1 + InGameManager.Instance.Player.StatSystem.GetStat(StatNames.IncreaseHighGradeRate);

        switch (grade)
        {
            case GradeNames.Mythic:
                return 0.004f * addChance;

            case GradeNames.Legendary:
                return 0.021f * addChance;

            case GradeNames.Unique:
                return 0.057f * addChance;

            case GradeNames.Magic:
                return 0.123f * addChance;

            case GradeNames.Rare:
                return 0.246f * addChance;

            case GradeNames.Normal:
                return 0.55f;

            default:
                return 1f;
        }
    }
}