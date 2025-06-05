using System;
using TMPro;
using UnityEngine;

public static class EXText
{
    public static readonly Color lightCyan = new Color(0.5f, 1f, 1f, 1f);

    public static string GetGradeColor(GradeNames type, string text)
    {
        return string.Format("<style={0}>{1}</style>", type.ToString(), text);
    }

    public static string GetStatPercent(StatNames stat, float value)
    {
        switch (stat)
        {
            case StatNames.Attack:
            case StatNames.Armor:
            case StatNames.Health:
            case StatNames.AddRerollTrait:
            case StatNames.AttackSpeed:
                {
                    return value.ToString();
                }

            default:
                return string.Format("{0}%", value * 100f);
        }
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

            case StatNames.ExpGainRate:
                return "경험치 획득량";

            case StatNames.DamageReduction:
                return "피해 감소";

            case StatNames.DamageRate:
                return "피해 증가";

            case StatNames.DodgeRate:
                return "회피율";

            case StatNames.AllStats:
                return "올스텟";
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

    public static string GetEliteLanguage(this EliteTypes type)
    {
        switch (type)
        {
            case EliteTypes.Powerful:
                return "강력한";

            case EliteTypes.Armored:
                return "단단한";

            case EliteTypes.Resilient:
                return "질긴";

            case EliteTypes.Agile:
                return "재빠른";

            case EliteTypes.Deadly:
                return "치명적인";
        }

        return "";
    }

    public static string GetEliteDescLanguage(this EliteTypes type)
    {
        switch (type)
        {
            case EliteTypes.Powerful:
                return "공격력 증가";

            case EliteTypes.Armored:
                return "방어력 증가";

            case EliteTypes.Resilient:
                return "체력 증가";

            case EliteTypes.Agile:
                return "회피율 증가";

            case EliteTypes.Deadly:
                return "치명타 증가";
        }

        return "";
    }

    public static string GetClassLanguage(this ClassNames name)
    {
        switch (name)
        {
            case ClassNames.Swordman:
                return "검사";

            case ClassNames.Gladiator:
                return "검투사";

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

            case ClassNames.SwordMaster:
                return "소드마스터";

            case ClassNames.LordKnight:
                return "로드나이트";

            case ClassNames.Paladin:
                return "팔라딘";

            case ClassNames.Revenger:
                return "리벤저";

            case ClassNames.ArchDruid:
                return "아크드루이드";

            case ClassNames.TimeTraveler:
                return "타임 트래블러";

            case ClassNames.ShadowDancer:
                return "섀도우댄서";

            case ClassNames.StormTrooper:
                return "스톰트루퍼";

            case ClassNames.Necromancer:
                return "네크로맨서";

            case ClassNames.Phantom:
                return "팬텀";

            case ClassNames.GoldMaker:
                return "골드메이커";

            case ClassNames.MasterCraftsman:
                return "명장";

            case ClassNames.Arcana:
                return "아르카나";
        }

        return "";
    }

    public static float GetValueByRound(float value)
    {
        return (float)Math.Round(value, 2);
    }

    public static void RefreshClassValueText(ClassData classData, TextMeshProUGUI text)
    {
        Player player = InGameManager.Instance.Player;

        string value = "[" + classData.NameText + "]" + "\n";

        if (classData.Stats.Count == 1)
        {
            value += string.Format(classData.StatDescriptionText, EXText.GetStatPercent(classData.Stats[0], classData.Values[0])) + "\n";
        }
        else if (classData.Stats.Count == 2)
        {
            value += string.Format(classData.StatDescriptionText,
                EXText.GetStatPercent(classData.Stats[0], classData.Values[0]), EXText.GetStatPercent(classData.Stats[1], classData.Values[1])) + "\n";
        }

        if (classData.Projectiles.Count == 1)
        {
            value += string.Format(classData.ProjectileDescritonText, player.ProjectileSystem.GetProjectile(classData.Projectiles[0]).Chance * 100f, player.ProjectileSystem.GetProjectile(classData.Projectiles[0]).DamgeRate * 100f) + "\n";
        }
        else if (classData.Projectiles.Count == 2)
        {
            value += string.Format(classData.ProjectileDescritonText, player.ProjectileSystem.GetProjectile(classData.Projectiles[0]).Chance * 100f, player.ProjectileSystem.GetProjectile(classData.Projectiles[0]).DamgeRate * 100f,
                player.ProjectileSystem.GetProjectile(classData.Projectiles[1]).Chance * 100f, player.ProjectileSystem.GetProjectile(classData.Projectiles[1]).DamgeRate * 100f) + "\n";
        }

        if (classData.BuffNames.Count == 1)
        {
            value += string.Format(classData.BuffDescritonText, classData.BuffValues[0] * 100f);
        }
        else if (classData.BuffNames.Count == 2)
        {
            value += string.Format(classData.BuffDescritonText, classData.BuffValues[0] * 100f, classData.BuffValues[1] * 100f);
        }
        else if (classData.BuffNames.Count == 3)
        {
            value += string.Format(classData.BuffDescritonText, classData.BuffValues[0] * 100f, classData.BuffValues[1] * 100f, classData.BuffValues[2] * 100f);
        }

        text.SetText(value);
    }
}