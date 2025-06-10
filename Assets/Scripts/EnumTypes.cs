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

public enum EliteTypes
{
    None,
    Powerful, //강력한
    Armored, // 단단한
    Resilient,//질긴
    Agile, //날렵한
    Deadly,//치명적인
}

public enum ClassNames
{
    None,

    // Tier 0

    Swordman, //검사

    Gladiator = 10, // 검투사
    Knight, // 나이트
    Crusader,//크루세이더
    Berserker, //버서커
    Druid, // 드루이드

    Tracer, // 트레이서
    Rogue, // 로그
    Hunter, //헌터
    DarkKnight, //암흑기사

    Gambler, // 도박사
    Merchant, // 상인
    Blacksmith, // 장인
    Diviner, // 점술사

    //Tier 1

    SwordMaster = 100, // 소드마스터(무력)
    LordKnight, // 로드나이트(규율)
    Paladin, // 팔라딘(신성)
    Revenger, // 리벤저(분노)
    ArchDruid, // 아크드루이드(야성)

    TimeTraveler, // 타임트래블러(신속)
    StormTrooper, // 스톰트루퍼(정밀)
    ShadowDancer, // 섀도우댄서(암살)
    Necromancer, // 네크로맨서(어둠)

    Phantom, // 팬텀(행운)
    GoldMaker,// 골드메이커(감각)
    MasterCraftsman, // 명장(장인)
    Arcana,// 아르카나(운)
}

public enum ClassTraitNames
{
    None,

    //Tier1

    Might, // 무력 – 무투사 (Brawler)
    Discipline, // 규율 – 나이트 (Knight)
    Divinity, // 신성 – 크루세이더 (Crusader)
    Fury, // 분노 – 버서커 (Berserker)
    Feral, // 야성 – 드루이드 (Druid)

    Swiftness, // 신속 – 트레이서 (Tracer)
    Assassination, // 암살 – 로그 (Rogue)
    Precision, // 정밀 – 헌터 (Hunter)
    Darkness, // 어둠 – 암흑기사 (Dark Knight)

    Fortune, // 행운 – 겜블러 (Gambler)
    Insight, // 감각 – 상인 (Merchant)
    Expertise, // 숙련 – 대장장이 (Blacksmith)
    Fate, // 운명 – 점술사 (Diviner)
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

    Paladin,
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
    AttackSpeedRate,
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
    ExpGainRate,
    IncreaseHealingOnHitChance,

    LimitHealth,
    DoubleAttackSpeed,
    RandomWeaponSkill,
    DamageRate,
    WeaponSkillDamageRate,

    IncreaseHighGradeRate,
    DecreaseWeaponUpgradeCost,
    AddRerollTrait,

    DamageReflection,
    ArmorConvertToAttack,
    CriticalDamageConvertToAttackRate,
    IgnoreArmor,

    AttackToAddGoldChance,
    PassiveSkillLearnChance,
    AccOptionRerollFreeGem,
    AddedWaeponStat,
    FailToWeaponChance,
    Invincibility,
    AllStats,
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

public enum GradeNames
{
    None,
    Mythic,
    Legendary,
    Unique,
    Magic,
    Rare,
    Normal,
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

    //Class

    LordKnight,
    Paladin,
    Paladin2,
    ArchDruid,
    TimeTravelerBuff,
    TimeTravelerDebuff,
    Necromancer,

    Arcana1, //악마
    Arcana2, //달빛
    Arcana3, //운명
}

public enum BuffTypes
{
    None,
    Stat,
    Trigger,
    Stack,
    Heal,
}

public enum ApplyTypes
{
    None,
    Owner,
    Target,
    All,
}

public enum SkillConditions
{
    None,
    Attack,
    Demaged,
    Killed,
    Dead,
    AddedGold,
    Heal,
    CriticalAttack,
}

public enum BuffConditions
{
    None,
    MonsterSpawnd,
    PlayerAttackToNoCritical,
    PlayerAttackToCritical,
    PlayerDamaged,
    DecreaseHealth,
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
    UsingHeal, // 힐 발동
    LevelUp, // 플레이어 레벨업
    RefreshPlayerHP, // 플레이어 HP 변화
    RefreshPlayerStst, // 플레이어 스텟 변화
    SkillLevelUp, // 스킬 레벨업
    MonsterSpawnd,//몬스터 스폰
    ChangeMonsterLevel, // 몬스터 레벨 변화
    MonsterDead, //몬스터 사망.

    AddCurrency, // 재화획득
    UseCurrency, // 재화사용

    RefreshAccessory, // 악세서리 변화

    RefreshAttackSpeed, // 스텟 혹은 배속에 따른 공격속도 변화
    AddTrait, // 특성 획득
    RefreshMemory,//기억 저장소 변화
    ChangeClass,// 클래스 전직
}

public enum DamageTypes
{
    None,
    Attack,
    WeaponSkill,
    Proejctile,
    Reflect,
}

public enum ProjectileNames
{
    None,
    SwordMaster,
    StormTrooper,
    StormTrooperBig,
    Paladin,
    RevengerAttack,
    RevengerHit,
    ArchDruid,
    GoldMaker,
    ShadowDancer,
}

public enum ProjectileMoveTypes
{
    None,
    Linear,
    Curved,
    Stop,
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
    Memory,
    Spawner,
    Challenge,
    Elite,
    Weapon,
    PassiveSkill,
    PassiveSkillMaxLevel,
    Necklace,
    Ring,
    Other,
    Buff,
    BuffStack,
    Class1,
    Class2,
    Class3,
    Convert,
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

public enum UIPopupNames
{
    None,
    ClassTrait, //특성 선택 팝업
    GameDescription, //특성 선택 팝업
    Memory, // 기억 저장소 팝업
    Challenge, // 도전과제 팝업
    GamePause, // 게임정지팝업
}

public enum UIDetailsNames
{
    None,
    WeaponUpgrade,
}

public enum UIChallengeNames
{
    None,
    DiligentWorker,
    PocketStronghold,
    Rookie,
    WeaponEnthusiast,
    NthReincarnation,

    //Title

    TraceOfTheFirstMark = 100, //처음새긴흔적
    FootprintsLeftBehind,//남겨진 발자국
    RemnantsOfWillpower,//의지의 잔흔
}

public enum LanguageTypes
{
    None,
    Korea,
    English,
}

public enum SoundNames
{
    None,
    ButtonClick,
    Slash1,
    Slash2,
    Success,
    WeaponUpgrade,
    LevelUp,
    Dead,

    //BGM
    TitleBGM,
}