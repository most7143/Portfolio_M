public enum CharacterNames
{
    None,
    Swordman,
    BoneWormBlack,
    BoneWormBlue,
    BoneWormGreen,
    BoneWormWhite,

    SlimeGreen,
    SlimeBlue,
    SlimeRed,
    SlimeBlack,

    RatBrown,
    RatGreen,
    RatBlack,
    RatWhite,

    SpiderGreen,
    SpiderBlue,
    SpiderBlack,
    SpiderWhite,

    ScorpionGreen,
    ScorpionBlue,
    ScorpionBlack,
    ScorpionWhite,

    WolfGreen,
    WolfBlue,
    WolfBlack,
    WolfWhite,

    CerberusGreen,
    CerberusRed,
    CerberusBlack,
    CerberusWhite,

    SkullGreen,
    SkullBlue,
    SkullRed,
    SkullViolet,

    End,
}

public enum WeaponNames
{
    None,
    WoodenSword,
    Dagger,
    IronSword,
    GoldenIronSword,
    DoubleEdgedSword,
    BlackIronGreatsword,
    Kris,
    Plumblossom,
    Soulbeheader,
    SwordOfGlory,
    Phoenix,
    DoomsDay,
}

public enum StringTypes
{
    None,
    Player,
    Monster,
    Weapon,
}

public enum DataTypes
{
    None,
    Monster,
    Stage,
    Weapon,
}

public enum StageNames
{
    None,
    Forest,
    Forest2,
    Forest3,
    Forest4,
    End,
}

public enum UINames
{
    None,
    UIFade,
}

public enum FXSpawnTypes
{
    None,
    Target,
    TargetRandomRange,
    Owner,
}

public enum FXNames
{
    None,
    FireBomb,
    LightBomb,
    FireBombExplosion,
    PinkCut,
    DeathSlash,
    Cut,
    DeathWaltz,
    SavageClaw,
    Slaughter,
    CrimsonThunder,
    CrimsonDoom,
    FireCut,
}

public enum WeaponSkillNames
{
    None,
    FireBomb,
    LightBomb,
    FireBombExplosion,
    PinkCut,
    DeathSlash,
    Cut,
    DeathWaltz,
    SavageClaw,
    Slaughter,
    CrimsonThunder,
    CrimsonDoom,
    FireCut,
}

public enum StatNames
{
    None,

    Attack,
    AttackRate,
    CriticalChance,
    CriticalDamage,
    AttackSpeed,
    HealingOnHit,

    Health,
    HealthRate,

    Armor,
    ArmorRate,
    DamageReduction,

    DodgeRate,

    WeaponTriggerChance,
    CurrencyGainRate,
}

public enum PassiveSkillNames
{
    None,

    //Rank1

    StaminaTraining,  // 체력단련 - 체력 증가
    StrengthTraining, // 근력단련 - 공격력 증가

    //Rank2

    Durability, // 강인함 - 받는 피해 % 감소
    WeaknessExposure, // 약점 노출 - 치명타 확률 % 증가
    SurvivalOfTheFittest,// 약육 강식 - 치명타 데미지 % 증가

    //Rank3

    Vampire, //흡혈귀 - 일반공격시 5% 확률로 준 %회복
    Gale, //돌풍 - 공격속도 n% 증가
    TreasureHunter, // 보물 사냥꾼 - 재화 획득량 %증가

    //Rank4

    CursedTome, //저주받은 마도서 - 최대 체력이 70%로 제한되지만, 공격력 50% 상승
    Enforcer, // 집행자 - 몬스터 조우 시 5초 동안 피해를 입지 않음.
    PowerOfGenesis, //창세의 힘 - 무기스킬 발동 확률 100% 증가
}

public enum PassiveGrades
{
    None,
    Normal,
    Rare,
    Unique,
    Legendary,
}

public enum SkillTypes
{
    None,
    Attack,
    Buff,
}

public enum SkillConditions
{
    None,
    Attack,
    Demaged,
    Killed,
    Dead,
}

public enum EventTypes
{
    None,
    AttackExecuted,
    PlayerDamaged,
    MonsterDefeated,
    EquipedWeapon,
    AttackSkill,
}

public enum DamageTypes
{
    None,
    Attack,
    Skill,
}