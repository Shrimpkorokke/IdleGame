using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSheet.Core.Type;

[UGS(typeof(GrowthType))]
public enum GrowthType
{
    Training,
    Ability
}

[UGS(typeof(TrainingGrade))]
public enum TrainingGrade
{
    Normal,
    Super
}

[UGS(typeof(TrainingType))]
public enum TrainingType
{
    AttPower,
    AttSpeed,
    CriRate,
    CriDamageRate,
    FinalDamageRate
}

[UGS(typeof(AbilityType))]
public enum AbilityType
{
    AttPower,
    NormalAddDmg,
    BossAddDmg,
    FiveAttack,
    Execution,
    FinalDamageRate
}
