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

    StaminaTraining,  // ü�´ܷ� - ü�� ����
    StrengthTraining, // �ٷ´ܷ� - ���ݷ� ����

    //Rank2

    Durability, // ������ - �޴� ���� % ����
    WeaknessExposure, // ���� ���� - ġ��Ÿ Ȯ�� % ����
    SurvivalOfTheFittest,// ���� ���� - ġ��Ÿ ������ % ����

    //Rank3

    Vampire, //������ - �Ϲݰ��ݽ� 5% Ȯ���� �� %ȸ��
    Gale, //��ǳ - ���ݼӵ� n% ����
    TreasureHunter, // ���� ��ɲ� - ��ȭ ȹ�淮 %����

    //Rank4

    CursedTome, //���ֹ��� ������ - �ִ� ü���� 70%�� ���ѵ�����, ���ݷ� 50% ���
    Enforcer, // ������ - ���� ���� �� 5�� ���� ���ظ� ���� ����.
    PowerOfGenesis, //â���� �� - ���⽺ų �ߵ� Ȯ�� 100% ����
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