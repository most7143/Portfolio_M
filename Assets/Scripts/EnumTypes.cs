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

    StrengthTraining, // �ٷ´ܷ� - ���ݷ� ����
    StaminaTraining,  // ü�´ܷ� - ü�� ����
    BlockingTraining, // �����Ʒ� - ���� ����

    //Rank2

    OmniDirectionalMobility, // ��ü �⵿ - ȸ���� % ����
    WeaknessExposure, // ���� ���� - ġ��Ÿ Ȯ�� % ����
    SurvivalOfTheFittest,// ���� ���� - ġ��Ÿ ������ % ����

    //Rank3

    Durability, // ������ - �޴� ���� % ����
    Vampire, //������ - �Ϲݰ��ݽ� 5% Ȯ���� �� %ȸ��
    Gale, //��ǳ - ���ݼӵ� n ����
    TreasureHunter, // ���� ��ɲ� - ��ȭ ȹ�淮 %����

    //Rank4

    CursedTome, //���ֹ��� ������ - �ִ� ü���� 70%�� ���ѵ�����, ���ݷ� 100% ���
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
    WeaknessExposure, // �������� - ġ��Ÿ ���� ���� Ȯ�� ����
    Enforcer,// ������ - ���� ���� �� 5�� ���� ���ظ� ���� ����.
    Enforcer2,// �Ұ��� ���� - ���� ���� �� 5�� ���� ���� ���� ���� , ���ӽð� 3�� ������ �ִ� ���� ����
    Durability, // ö�� - ���� ���Ϳ��� ���ظ� ���� �� ���� ���� ����
    Gale, // ���̿÷ν��� ���� -6�ʵ��� ���ݼӵ� 2��� ���� 6�� ��
    CursedTome, // �������� ���� - ���� ü���� 70% ����
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
    PlayerAttackExecuted, // ����
    PlayerAttackToNoCritical, // ���� �� ġ��Ÿ�� �ƴ� ���
    PlayerAttackToCritical, // ���� �� ġ��Ÿ�� ���
    PlayerDamaged, // �÷��̾� ��������
    EquipedWeapon, // ��� ����
    AttackSkill, // ���� ��ų �ߵ�
    LevelUp, // �÷��̾� ������
    RefreshPlayerHP, // �÷��̾� HP ��ȭ
    RefreshPlayerStst, // �÷��̾� ���� ��ȭ
    SkillLevelUp, // ��ų ������
    MonsterSpawnd,//���� ����
    ChangeMonsterLevel, // ���� ���� ��ȭ
    MonsterDead, //���� ���.

    AddCurrency, // ��ȭȹ��
    UseCurrency, // ��ȭ���
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