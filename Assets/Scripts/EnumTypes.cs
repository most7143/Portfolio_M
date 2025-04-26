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

public enum AccessoryTypes
{
    None,
    Necklace,
    Ring,
    Other,
}

public enum CharacterTypes
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

    AttackByLevel,
    HealthByLevel,
    ArmorByLevel,

    WeaponTriggerChance,
    CurrencyGainRate,
    IncreaseHealingOnHitChance,

    LimitHealth,
    DoubleAttackSpeed,
    RandomWeaponSkill,
    DamageRate,
}

public enum PassiveSkillNames
{
    None,

    //Rank1

    StrengthTraining, // 근력단련 - 공격력 증가
    StaminaTraining,  // 체력단련 - 체력 증가
    BlockingTraining, // 막기훈련 - 방어력 증가

    //Rank2

    OmniDirectionalMobility, // 입체 기동 - 회피율 % 증가
    WeaknessExposure, // 약점 노출 - 치명타 확률 % 증가
    SurvivalOfTheFittest,// 약육 강식 - 치명타 데미지 % 증가

    //Rank3

    Durability, // 강인함 - 받는 피해 % 감소
    Vampire, //흡혈귀 - 일반공격시 5% 확률로 준 %회복
    Gale, //돌풍 - 공격속도 n 증가
    TreasureHunter, // 보물 사냥꾼 - 재화 획득량 %증가

    //Rank4

    CursedTome, //저주받은 마도서 - 최대 체력이 70%로 제한되지만, 공격력 100% 상승
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

public enum GradeTypes
{
    None,
    Normal,
    Rare,
    Magic,
    Unique,
    Legendary,
    Mythic,
}

public enum PassiveSkillTypes
{
    None,
    Stack,
}

public enum SkillTypes
{
    None,
    Attack,
    Buff,
}

public enum BuffNames
{
    None,
    WeaknessExposure, // 약점노출 - 치명타 실패 마다 확률 증가
    Enforcer,// 집행자 - 몬스터 조우 시 5초 동안 피해를 입지 않음.
    Enforcer2,// 불공정 집행 - 몬스터 조우 시 5초 동안 피해 입지 않음 , 지속시간 3초 증가와 주는 피해 증가
    Durability, // 철갑 - 같은 몬스터에게 피해를 받을 때 마다 방어력 증가
    Gale, // 아이올로스의 숨결 -6초동안 공격속도 2배로 증가 6초 쿨
    CursedTome, // 벨리알의 마서 - 적의 체력을 70% 제한
}

public enum BuffTypes
{
    None,
    Stat,
    Trigger,
    Stack,
}

public enum SkillConditions
{
    None,
    Attack,
    Demaged,
    Killed,
    Dead,
    AddedGold,
}

public enum BuffConditions
{
    None,
    MonsterSpawnd,
    PlayerAttackToNoCritical,
    PlayerAttackToCritical,
    PlayerDamaged,
    CoolDown,
}

public enum EventTypes
{
    None,
    PlayerAttackExecuted, // 공격
    PlayerAttackToNoCritical, // 공격 시 치명타가 아닌 경우
    PlayerAttackToCritical, // 공격 시 치명타인 경우
    PlayerDamaged, // 플레이어 피해입음
    EquipedWeapon, // 장비 장착
    AttackSkill, // 무기 스킬 발동
    LevelUp, // 플레이어 레벨업
    RefreshPlayerHP, // 플레이어 HP 변화
    RefreshPlayerStst, // 플레이어 스텟 변화
    SkillLevelUp, // 스킬 레벨업
    MonsterSpawnd,//몬스터 스폰
    ChangeMonsterLevel, // 몬스터 레벨 변화
    MonsterDead, //몬스터 사망.

    AddCurrency, // 재화획득
    UseCurrency, // 재화사용
}

public enum DamageTypes
{
    None,
    Attack,
    Skill,
}

public enum LogTypes
{
    None,
    Stat,
    Character,
    Damage,
    Attack,
    Skill,
    Buff,
}

public enum StatTID
{
    None,
    Base,
    Weapon,
    PassiveSkill,
    PassiveSkillMaxLevel,
    Buff,
    BuffStack,
}

public enum CurrencyTypes
{
    None,
    Gold,
    Gem,
}

public enum PocketTypes
{
    None,
    Yellow,
    Green,
    Red,
    Black,
}