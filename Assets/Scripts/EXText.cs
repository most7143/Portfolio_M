public static class EXText
{
    public static string SetGradeColor(GradeNames type, string text)
    {
        return string.Format("<style={0}>{1}</style>", type.ToString(), text);
    }

    public static string GetStatLanguage(this StatNames stat)
    {
        switch (stat)
        {
            case StatNames.Attack:
            case StatNames.AttackRate:
                {
                    return "공격력";
                }

            case StatNames.Armor:
            case StatNames.ArmorRate:
                {
                    return "방어력";
                }

            case StatNames.Health:
            case StatNames.HealthRate:
                {
                    return "체력";
                }

            case StatNames.AttackSpeed:
                return "공격속도";

            case StatNames.CriticalChance:
                return "치명타 확률";

            case StatNames.CriticalDamage:
                return "치명타 피해";

            case StatNames.CurrencyGainRate:
                return "재화 획득량";

            case StatNames.DamageReduction:
                return "피해 감소";

            case StatNames.DamageRate:
                return "피해 증가";

            case StatNames.DodgeRate:
                return "회피율";
        }

        return "";
    }

    public static string GetGradeLanguage(this GradeNames grade)
    {
        switch (grade)
        {
            case GradeNames.Mythic:
                return "미스틱";

            case GradeNames.Legendary:
                return "레전더리";

            case GradeNames.Unique:
                return "유니크";

            case GradeNames.Magic:
                return "매직";

            case GradeNames.Rare:
                return "레어";

            case GradeNames.Normal:
                return "노말";
        }

        return "";
    }

    public static string GetTraitLanguage(this ClassNames name)
    {
        switch (name)
        {
            case ClassNames.Swordman:
                return "검사";

            case ClassNames.Brawler:
                return "무투사";

            case ClassNames.Knight:
                return "나이트";

            case ClassNames.Crusader:
                return "크루세이더";

            case ClassNames.Berserker:
                return "버서커";

            case ClassNames.Druid:
                return "드루이드";

            case ClassNames.Tracer:
                return "트레이서";

            case ClassNames.Rogue:
                return "로그";

            case ClassNames.Hunter:
                return "헌터";

            case ClassNames.DarkKnight:
                return "암흑기사";

            case ClassNames.Gambler:
                return "도박사";

            case ClassNames.Merchant:
                return "상인";

            case ClassNames.Blacksmith:
                return "대장장이";

            case ClassNames.Diviner:
                return "점술사";
        }

        return "";
    }
}